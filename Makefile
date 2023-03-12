docker-image:
	docker build -t savemeter-api .

mongo-backup-dev:
	mongodump --config=mongoConfig.yaml

mongo-restore-dev:
	mongorestore --config=mongoConfig.yaml --drop dump/ 

mongo-restore-local:
	mongorestore --drop dump/
