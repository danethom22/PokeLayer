<div id="top"></div>

  <h3 align="center">PokeLayer</h3>

  <p align="center">
    A TrueLayer pokemon API challenge
    <br />
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

This project was created to complete a challenge

The requirements were:
* Fetch pokemon based resources from a 3rd party Pokemon API
* Apply Fun translations to the pokemons description depending on certain criteria
* Build an API and expose the above resources via endpoints


### Built With

The following languages and frameworks were used to complete the project

* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [.Net core](https://dotnet.microsoft.com/en-us/download/)
* [Docker](https://www.docker.com/)

Along with the following Nuget packages

* Newtonsoft.Json (13.0.1)
* Swashbuckle.AspNetCore (6.2.3)
* Moq (4.16.1)
* xUnit (2.4.1)


<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

Get the project up and running with these steps

### Prerequisites

This project will require

* [.Net Core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1/)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Installation

1. Install [.Net Core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1/)
2. Install [Docker Desktop](https://www.docker.com/products/docker-desktop/)
3. Clone the code from this repository

   ```sh
   git clone https://github.com/danethom22/PokeLayer.git
   ```

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- USAGE EXAMPLES -->
## Usage

The API exposes two endpoints, namely: /pokemon/{pokemon name} and /pokemon/translated/{pokemon name}
These endpoints allow a consumer of the API to receive information about a pokemon as well as apply a translation to the pokemons description based on certain characteristics.
The project can be run in two ways.

### Through a terminal

The project can be run by
1. Opening a terminal and navigating to your local repository directory, for example if you had cloned the repository into c:/source/projects/pokelayer
  the command would be:
  
   ```sh
   cd c:/source/projects/pokelayer
   ```
2. Navigating further to the PokeLayer.Api folder in which the .csproj file is found:

   ```sh
   cd PokeLayer.Api
   ```
3. Executing the 'dotnet run' command:

   ```sh
   dotnet run
   ```

This will start the project on ports 5000 and 5001 which can be found in a browser at https://localhost:5000/ and https://localhost:5001/
Alternatively the API can be utilized through a swagger page on either https://localhost:5000/swagger/index.html or https://localhost:5001/swagger/index.html

### In a Docker container

1. Open Docker desktop on your machine
2. Opening a terminal and navigating to your local repository directory, for example if you had cloned the repository into c:/source/projects/pokelayer
  the command would be:
  
   ```sh
   cd c:/source/projects/pokelayer
   ```
   
3. Run the following command which will execute commands in the Dockerfile and create a Docker image with the tag 'PokeLayer'

   ```sh
   docker build -t PokeLayer .
   ```
   
4. Once the Docker image has been created, run the next command to start the container on your machine. This will start the PokeLayer image under the name 'PokeLayer'
   ```sh
   docker run -d -p 8080:80 --name PokeLayer pokelayer
   ```
   
Once complete, the project should now be running on port 8080 and can be accessed by opening a browser and navigating to https://localhost:8080/. This will simulate a 
Production environment and swagger will not be available, however the endpoints will be accessable through a browser.

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- IMPROVEMENTS -->
## Improvements

Given more time, and for a production environment, the project could be greatly enhanced. Some features to add would be:
* Tracking ID's to follow requests through the system to allow better debugging
* Improved use of logging 
* Better work flow actions to ensure unit tests have been run prior to all commits
* Integration testing to test integrations with 3rd party API's


<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* Thank you to [TrueLayer](https://truelayer.com/) for the opportunity

<p align="right">(<a href="#top">back to top</a>)</p>
