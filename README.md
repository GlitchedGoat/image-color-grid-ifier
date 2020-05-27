# image-color-grid-ifier
Convert color images into greyscale ones with a color grid overlaid.

Built in .Net core 3.1.3. To build, simply do a `dotnet restore` and a `dotnet build`. Run the resulting `image-color-grid-izer.exe` in the bin directory.

Arguments:
```
  -f, --filepath          Required. Path of the image to convert

  -o, --outputfilepath    Path of the resulting image to write

  -s, --spacing           grid size; how often to draw a full-color pixell. default 5px

  -l, --linewidth         line width, how wide each lin is. default 1px.

  --help                  Display the help screen.

  --version               Display version information.
  ```
  
  Example:
  ![Example of grid-ified image](example-result.png)
