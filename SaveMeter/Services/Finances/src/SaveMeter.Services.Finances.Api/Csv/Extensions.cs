using CsvHelper;
using CsvHelper.Configuration;

namespace SaveMeter.Services.Finances.Api.Csv
{
    public static class Extensions
    {
        public static bool CanRead<T>(this CsvReader reader) where T : ClassMap, new()
        {
            var classMap = new T();
            var headers = reader.HeaderRecord.ToList();
            var canReads = classMap.MemberMaps.Select(x => headers.Exists(y => B(y, x))).ToList();
            var canRead = canReads.Count(x => x) == classMap.MemberMaps.Count(x => x.Data.Names.Any());
            return canRead;
        }

        private static bool B(string y, MemberMap x)
        {
            return y == x.Data.Names.FirstOrDefault();
        }
    }
}
