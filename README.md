# CheckActionAPI
A small Stardew Valley API to help with adding custom functionality to items.

## Methods
Currently this API only provides one method, accessed through the `CheckAction` class.

|Method Name|Description|
|---|---|
|`Check`|Returns true if the player's input should trigger the currently held item to be used.|

## Usage
Download the dll from **Releases** and place it somewhere inside your project folder.

Add the following inside the `<Project>` tag of your `.csproj` file, replacing the path so it correctly points to the dll file:  
```xml
<ItemGroup>
    <Reference Include="CheckActionAPI">
        <HintPath>{path}/CheckActionAPI.dll</HintPath>
    </Reference>
</ItemGroup>
```

Then add the following line inside the `<PropertyGroup>` tag:  
```xml
<BundleExtraAssemblies>ThirdParty</BundleExtraAssemblies>
```

Finally, add the following to each `.cs` file in which you wish to make use of the API:  
```c#
using CheckActionAPI;
```

You should now be able to access the `CheckAction` class in your project.
