# Case_Study_Inventory

##Instructions

###Deployment Instruction

1. We should make sure to use a development certificate,
otherwise you can use a paid SSL certificate instead of the self signed and change and change the ASPNETCORE_ENVIRONMENT environement variable to 'Production'.

```bash
#Run the following command to create a development certificate

dotnet dev-cert https --clean
dotnet dev-cert https --trust
```

2. Build the application.

3. Right click on the dock-compose project and click `Compose Up`. This should create the images and host the container of our services.

4. Right click on the `portal` project and click on `Open Terminal`. Then run the following command which will create the `dist` folder.

```bash
npm run build
```

5. Navigate to the `dist` folder in the `portal` project root and open the `index.html` file in a notepad. Then `.` infront of every file path

```bash
/favicon.ico => ./favicon.ico
/js/chunk-vendors.b1fe5ad8.js => ./js/chunk-vendors.b1fe5ad8.js
/js/app.5906b097.js => ./js/app.5906b097.js
/css/chunk-vendors.b7c0e0b0.css => ./css/chunk-vendors.b7c0e0b0.css
/css/app.906a6b90.css => ./css/app.906a6b90.css
```

6. Then open the index.html in a browser and use the application
