# DotNetStarter
master   ![master](https://github.com/Ninds/DotNetStarter/workflows/Build%20and%20Test/badge.svg)

prelease ![prelease](https://github.com/Ninds/DotNetStarter/workflows/Build%20and%20Test/badge.svg?branch=prerelease)

release  ![release](https://github.com/Ninds/DotNetStarter/workflows/Build%20and%20Test/badge.svg?branch=release)

Template Starter Repo for a .NET Standard library with GitHub Actions workflow to unit test. build, & deploy nuget packages to Git Hub packages & nuget.org

Contains a .NET Class library and Unit tests.
Three branches are expected :
- master       - each push is is unit tested
- release      - each push is unit tested and then a nuget package is built which is declared as a release and pushed to Github Packages & nuget.org
- prerelease   - each push is unit tested and then a nuget package is built which is declared as a prerelease and pushed to Github Packages & nuget.org

The code to push nuget packages has been commented out in the workflow and will need to be uncommented 
```yaml
        name: Github Package
        uses: tanaka-takayoshi/nuget-publish-to-github-packages-action@v2.1
        with:
          nupkg-path:  "package_output/*.nupkg"
          repo-owner:  'Narinder Claire'
          gh-user:  'Ninds'
          token:  ${{ secrets.GITHUB_TOKEN }}
      - name: Push with dotnet
        run:dotnet nuget push package_output/*.nupkg --api-key ${{ secrets.NUGET_API_KEY }} --source https://api.nuget.org/v3/index.json

```
See [Publishing-NuGet-packages-with-GitHub-Actions-75](https://www.blexin.com/en-US/Article/Blog/Publishing-NuGet-packages-with-GitHub-Actions-75) for details about setting up the Nuget API key as a Github secret.

The project file will have to be updated with details about the project & URL of repository


## Project File
```xml
<Project  InitialTargets="SetVersion" Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    <Version>0.0.0-dev$([System.DateTime]::Now.ToString(yyMMddhhmm))</Version>
    <Title>Ryu.NET</Title>
    <Description>
      <![CDATA[ _Scratch project for testing ]]>
    </Description>
    <RepositoryUrl>https://github.com/Ninds/DotNetStarter</RepositoryUrl>
    <RepositoryCommit>$(GITHUB_SHA)</RepositoryCommit>
    <RepositoryBranch>$(GITHUB_REF)</RepositoryBranch>
  </PropertyGroup>

  <PropertyGroup Condition=" '$(GITHUB_REF)' == 'refs/heads/release' and '$(CI)' == 'true' ">
    <Version>$([System.DateTime]::Now.ToString(yy.MM.dd))</Version>
  </PropertyGroup>

  <PropertyGroup Condition=" '$(GITHUB_REF)' == 'refs/heads/prerelease' and '$(CI)' == 'true'  ">
    <Version>$([System.DateTime]::Now.ToString(yy.MM.dd))-dev</Version>
  </PropertyGroup>

  <Target Name="SetVersion">
    <Message Text="::set-output name=version::$(Version)" Importance="high" />
  </Target>

</Project>
```
