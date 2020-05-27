# image-color-grid-ifier
Convert color images into greyscale ones with a color grid overlaid. The purpose is to show how human vision can "fill in" the gaps of a greyscale image to make it appear as if the whole thing were in color.

Built in .Net core 3.1.3. To build, simply do a `dotnet restore` and a `dotnet build` (or `dotnet publish-release`). Run the resulting `image-color-grid-izer.exe` in the bin directory.

Arguments:
```
  -f, --filepath           Required. Path of the image to convert

  -o, --outputfilepath     Path of the resulting image to write. Default is <original-file>-converted.png

  -s, --spacing            Grid size; how often to draw a full-color pixel. Default 5px

  -l, --linewidth          Line width, how wide each line is. Default 1px.

  -c, --colorsaturation    How much to over-saturate colors, as a multiplier. 1.0 is unchanged and 2.5 is default.

  --help                   Display this help screen.

  --version                Display version information..
  ```
  
  The example below has no color pixels, aside from the grid:

  ![Example of grid-ified image](example-output.png)
