docker build --build-arg buildtime_env=Stage -t swecvi_release_stage -f Dockerfile .
docker compose -f docker-compose-release-stage.yml down
docker compose -f docker-compose-release-stage.yml up -d