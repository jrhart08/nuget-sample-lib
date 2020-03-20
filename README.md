# nuget-sample-lib

## Deploying to a local folder

What we'll be doing in this demo. If your client is small (or cheap) a simple network folder
could act as their NuGet repository.

Deploying to a local folder is simple:

```bash
##  from NdashLib folder

# clear our release artifacts
dotnet clean --configuration Release

# build our project
dotnet build --configuration Release

# create our .nupkg file. "--include-source" will include a .symbols.nupkg file
dotnet pack --configuration Release --include-source

# push to our repository
dotnet nuget push .\bin\Release\*.nupkg --source C:\nuget-repo
```

**Note**: Add `*.nupkg` to your `.gitignore` if it isn't already there.

## Deploying to Artifactory

Deploying to an Artifactory repository is very similar to deploying to a local folder.


```bash
##  from NdashLib folder

# clear our release artifacts
dotnet clean --configuration Release

# build our project
dotnet build --configuration Release

# create our .nupkg file. "--include-source" will include a .symbols.nupkg file
dotnet pack --configuration Release --include-source

# push to our repository
dotnet nuget push .\bin\Release\*.nupkg --api-key <key> --source http://artifactory.my-client.com/artifactory/api/nuget/nuget
```

## Bonus: Setting up Artifactory locally

JFrog provides an open-source "freemium" version of Artifactory called Artifactory OSS.
It only supports a few package types and NuGet isn't one of them.
However, aside from choosing the repository type, general setup remains the same.
They provide several installation methods but we'll go with the Docker method here.

Other installation methods can be found here:
- Pro: https://jfrog.com/download-jfrog-platform/
- OSS: https://jfrog.com/open-source/

### Step 1: Install Docker

Windows: https://docs.docker.com/docker-for-windows/install/

Mac OS: https://docs.docker.com/docker-for-mac/install/

Ubuntu: https://docs.docker.com/install/linux/docker-ce/ubuntu/

### Step 2: Install artifactory Docker image

```bash
# artifactory pro
docker pull docker.bintray.io/jfrog/artifactory-pro

# artifactory OSS
docker pull docker.bintray.io/jfrog/artifactory-oss
```

### Step 3: Run image

```bash
# get GUID (image ID) of your artifactory image
docker images

# run image
docker run --detach -p 8081:8081 -p 8082:8082 -p 8046:8046 -p 8049:8049 --name artifactory <guid>
```

### Step 4: Create your NuGet Repository

Can be found at http://localhost:8081 (default login name/password: "admin/password")
- Skip to the "Repositories" portion of the setup wizard
- Select NuGet (Pro) or Generic (OSS)

### Step 5: Generate an API key

1. Click on "Welcome, admin" > "Edit Profile"
2. Enter your password
3. Scroll down to "API Key" and click "Generate"

### Step 6: Push to Artifactory

```bash
##  from NdashLib folder

# clear our release artifacts
dotnet clean --configuration Release

# build our project
dotnet build --configuration Release

# create our .nupkg file. "--include-source" will include a .symbols.nupkg file
dotnet pack --configuration Release --include-source

# push to our repository
dotnet nuget push .\bin\Release\*.nupkg --api-key <key> --source http://localhost:8081/artifactory/api/nuget/nuget
```
