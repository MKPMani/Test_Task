# takehome_test
# 1. Github source (public)
-------------------------
Clone the repository from Github
path: https://github.com/MKPMani/Test_Task.git

# 2. Enable Docker Desktop:
---------------------------
a) Navigate to the project root folder:
b) and type "cmd" from the folder path to run command to project folder
c) make sure the folder path contains docker-compose file there
d) run the docker command: docker compose up -d --build
c) it will run all the images to the docker container

# 3. Verify the docker image and container
verify the container after successful step 2. following information
a) kafka        - port : 9092:9092
b) kafka.ui     - port : 8080:8080
c) user-api     - port : 5001:8080
d) ordering-api - port : 5002:8080
all running status

# 4. Testing application on browser (swagger)
a) kafka ui     - http://localhost:8080/ui
b) user-api     - http://localhost:5001/index.html
c) ordering-api - http://localhost:5002/index.html

# 5. Test the scenario and verify kafka message in kafka-ui

# Pattern/Architecture used
a) Clean architecture
b) in-memory database Entity framework
c) Fluent validation, CQRS, mediatR
d) kafka even-driven messaging (pub/sub)
e) containersation - docker
b) xUnit - moq, mock 

# code referrences used chatgpt (copilot)
used for - kafka setup, kafka zookeeper, kafka ui setup, validation, consumer
few error code search on chatgpt
* Error creating broker listeners from 'PLAINTEXT://0.0.0.0:9092,CONTROLLER://0.0.0.0:29093': No security protocol defined for listener PLAINTEXT
* kafka-ui docker-compose setup without zookeeper
** many error code searched missed out here which is mostly kafka port number issues

# further improvement - (not completed)
* more test case scenario for all the interfaces
* more logging
* common project for kafka consumer
** known issues yet to fix and code commented - background consumer service for - (order-created)