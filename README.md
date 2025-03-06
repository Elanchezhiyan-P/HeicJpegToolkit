# HEIC to JPG Converter

This program allows you to convert HEIC images to JPG format using the **HeicJpegToolkit** library. Follow the instructions below to use the program.

## How to Use the HEIC to JPG Converter

### 1. Clone the Repository:
Clone the repository from GitHub:
```bash
git clone https://github.com/Elanchezhiyan-P/HeicJpegToolkit.git 
```

### 2. Set the Input File Path:
Locate the HEIC file you want to convert.
In the Main method, set the inputFilePath variable to the path of your HEIC file:

```csharp
string inputFilePath = "C:\\path\\to\\your\\file.heic";
```

### 3. Set the Output Folder Path:
Define the folder where you want to save the converted JPG image by setting the outputFolderPath variable:

```csharp
string outputFolderPath = "C:\\path\\to\\output\\folder";
```

### 4. Run the Program:
Once you have set the file paths, run the program. It will:

Check if the input file exists.
Verify that the input file is in HEIC format.
Create the output folder if it doesn't exist.
Convert the HEIC file to a JPG file and save it to the specified output folder.

### 5. View the Result:
After the conversion, if successful, the program will display the path of the saved JPG image. If the conversion fails, it will show an error message explaining the reason.


---


This program is based on the heic2jpg library, available at:  
[https://github.com/ejohnson-dotnet/heic2jpg](https://github.com/ejohnson-dotnet/heic2jpg)

## Author & Contact Information

**Author:** [Elanchezhiyan P](https://elanchezhiyan-p.is-a.dev/)

If you encounter any issues or bugs, please feel free to [open an issue](https://github.com/Elanchezhiyan-P/HeicJpegToolkit/issues).  
We are also open to any discussions, suggestions, or contributions! Don't hesitate to reach out.