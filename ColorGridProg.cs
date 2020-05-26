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

            [Option('o', "outputfilepath", Required = false, HelpText = "Path of the resulting image to write")]
            public string OutputFilepath {get; set;}

            [Option('s', "spacing", Required = false, HelpText = "grid size; how often to draw a full-color pixell. default 5px")]
            public int? PixelSpacing {get; set;}

            [Option('l', "linewidth", Required = false, HelpText = "line width, how wide each lin is. default 1px.")]
            public int? LineWidth {get; set;}
        }

        public static Image<Rgba32> loadImage(string filepath) {
            //
            System.Console.WriteLine($"Loading file: {filepath}");
            var loaded = Image.Load(filepath);
            return loaded.CloneAs<Rgba32>();
        }

        public static Image<Rgba32> convertImage(Image<Rgba32> input, int spacing, int linewidth) {
            System.Console.WriteLine($"Converting image of dimensions: {input.Width} by {input.Height}");
            // Note: GrayScale() can accept an arg
            // https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.Processing.GrayscaleExtensions.html
            var converted = input.Clone(s => s.Grayscale());

            // each pixel at once
            for (int x = 0; x < input.Width; x++) {
                for (int y = 0; y < input.Height; y++) {
                    var xSpacing = x % spacing;
                    var ySpacing = y % spacing;

                    // so if it is 0, that means match... otherwise allow + some number.
                    // can't be greater than the spacing... number of spacing is 0..spacing
                    // So if x/ySpacing is 0..lw
                    // if x/ySpacing < linewidth, allow it
                    if (xSpacing < linewidth || ySpacing < linewidth) {
                        // use original pixel color
                        converted[x, y] = input[x, y];
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

                    if (lineWidth >= pixelSpacing) {
                        System.Console.WriteLine($"pixel spacing of {pixelSpacing} cannot be >= line width of {lineWidth}!");
                    }
                    var img = loadImage(o.Filepath);
                    var converted = convertImage(img, o.PixelSpacing ?? 10, o.LineWidth ?? 1);
                    saveImage(converted, o.OutputFilepath ?? $"{o.Filepath}-converted.png");
                });
        }
    }
}
