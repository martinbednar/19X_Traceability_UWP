# How to run?

## 1. Install necessary tools

- Microsoft SQL Server
- SQL Server Management Studio (SSMS)
- Visual Studio

## 2. Create a new database

- Database name: 19X
- Collation: Czech_CI_AS
- Apply SQL queries in the file *SQL_CREATE_DB_19X.txt*

## 3. Enable Developer Mode for Windows

- Go to *Setting for developers* (Privacy & Security > For developers)
- Enable *Developer mode*

## 4. Update project to target an SDK that is installed

You might be asked while opening the solution in Visual Studio to update the SDK version in your project. Confirm the project update.

## 5. Generate a new certificate

- Double click the file *Package.appxmanifest* in the Visual Studio
- Select the tab *Packaging*
- Click the button *Choose Certificate*
- Start creating the new certificate by clicking *Create...*
- Enter a new password and click *OK*
- Confirm the certificate use by clicking *OK*

## 5. Build a package for the app deployment

- Right click the UWP project -> *Publish* -> *Create App Packages...*
- Select *Sideloading* -> *Next*
- *Trust* the certificate
- Use this trusted certificate for signing the app -> *Next* -> *Next*
- You have to add the *Installer location* even it will not be used. You can fill for example network path *\/\SFS\new\07_TEMP\MBE\App*.
- *Create*

## 5. Install the package

- Open the location: *19X_Traceability_UWP\AppPackages\19X_Traceability_UWP_1.0.2.0_x64_Test*
- Open the package *19X_Traceability_UWP_1.0.2.0_x64.msix*
- *Install*

## 6. Automatically run the app on startup

- Open the Start menu and find the newly installed app *19X_Traceability_UWP*
- Use *Drag & Drop* to create the app link on the Desktop
- Copy the link to *shell:startup* folder