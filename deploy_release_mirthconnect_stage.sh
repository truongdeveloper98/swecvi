docker build --build-arg buildtime_env=Stage -t swecvi_minthconnect_release_stage  -f Dockerfile.MirthConnect .
docker compose -f docker-compose-release-stage.yml down
docker compose -f docker-compose-release-stage.yml up -d