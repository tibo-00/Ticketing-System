# Use the SDK image to build and run tests
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS test

# Set the working directory in the container
WORKDIR /src

## Install Google Chrome
#RUN apt-get update && apt-get install -y wget gnupg2 software-properties-common
#RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add -
#RUN sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'
#RUN apt-get update && apt-get install -y google-chrome-stable
#
## Install ChromeDriver
#RUN CHROMEDRIVER_VERSION=`curl -sS chromedriver.storage.googleapis.com/LATEST_RELEASE` && \
    #wget -N http://chromedriver.storage.googleapis.com/$CHROMEDRIVER_VERSION/chromedriver_linux64.zip -P ~/ && \
    #unzip ~/chromedriver_linux64.zip -d ~/ && \
    #rm ~/chromedriver_linux64.zip && \
    #mv -f ~/chromedriver /usr/local/bin/chromedriver && \
    #chown root:root /usr/local/bin/chromedriver && \
    #chmod 0755 /usr/local/bin/chromedriver

# Copy csproj and restore any dependencies (via NuGet)
COPY *.sln .
COPY Tests/Tests.csproj Tests/
COPY ["Ticketing System/Ticketing System.csproj", "Ticketing System/"]
RUN dotnet restore

# Copy the project files and build your test project
COPY . .
RUN dotnet build --no-restore

# Define the entry point for the docker image.
# This will run the tests when the container starts.
WORKDIR /src/Tests
ENTRYPOINT ["dotnet", "test", "--logger:trx"]
