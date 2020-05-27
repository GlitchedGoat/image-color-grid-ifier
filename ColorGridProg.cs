using System;
using CommandLine;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace image_color_grid_izer
{
    class ColorGridProg
    {
        public class Options 
        {
            [Option('f', "filepath", Required = true, HelpText = "Path of the image to convert")]
            public string Filepath {get; set;}

            [Option('o', "outputfilepath", Required = false, HelpText = "Path of the resulting image to write. Default is <original-file>-converted.png")]
            public string OutputFilepath {get; set;}

            [Option('s', "spacing", Required = false, HelpText = "Grid size; how often to draw a full-color pixel. Default 5px")]
            public int? PixelSpacing {get; set;}

            [Option('l', "linewidth", Required = false, HelpText = "Line width, how wide each line is. Default 1px.")]
            public int? LineWidth {get; set;}

            [Option('c', "colorsaturation", Required = false, HelpText = "How much to over-saturate colors, as a multiplier. 1.0 is unchanged and 2.5 is default.")]
            public float? ColorSaturation {get; set;}
        }

        public static Image<Rgba32> loadImage(string filepath) {
            //
            System.Console.WriteLine($"Loading file: {filepath}");
            var loaded = Image.Load(filepath);
            return loaded.CloneAs<Rgba32>();
        }

        public static Image<Rgba32> convertImage(Image<Rgba32> input, int spacing, int linewidth, float saturation) {
            System.Console.WriteLine($"Converting image of dimensions: {input.Width} by {input.Height}");
            // Note: GrayScale() can accept an arg
            // https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.Processing.GrayscaleExtensions.html
            var converted = input.Clone(s => s.Grayscale());
            var source = input.Clone(s => s.Saturate(saturation));

            // each pixel at once
            for (int x = 0; x < source.Width; x++) {
                for (int y = 0; y < source.Height; y++) {
                    var xSpacing = x % spacing;
                    var ySpacing = y % spacing;

                    // so if it is 0, that means match... otherwise allow + some number.
                    // can't be greater than the spacing... number of spacing is 0..spacing
                    // So if x/ySpacing is 0..lw
                    // if x/ySpacing < linewidth, allow it
                    if (xSpacing < linewidth || ySpacing < linewidth) {
                        // use saturated
                        converted[x, y] = source[x, y];
                    }
                }
            }

            return converted;
        }

        public static void saveImage(Image<Rgba32> image, string filepath) {
            System.Console.WriteLine($"Saving output to file path {filepath}");
            image.Save(filepath);
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o => {
                    var pixelSpacing = o.PixelSpacing ?? 10;
                    var lineWidth = o.LineWidth ?? 1;
                    var saturation = o.ColorSaturation ?? 2.5f;

                    if (lineWidth >= pixelSpacing) {
                        System.Console.WriteLine($"pixel spacing of {pixelSpacing} cannot be >= line width of {lineWidth}!");
                    }
                    
                    try {
                        var img = loadImage(o.Filepath);
                        var converted = convertImage(img, pixelSpacing, lineWidth, saturation);
                        saveImage(converted, o.OutputFilepath ?? $"{o.Filepath}-converted.png");
                    } catch (System.IO.FileNotFoundException e) {
                        System.Console.WriteLine($"Couldn't open the file '{o.Filepath}' - check that you spelled it right!");
                    }
                });
        }
    }
}
