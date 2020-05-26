using System;
using CommandLine;
using SixLabors.ImageSharp;
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
        }

        public static Image<Rgba32> loadImage(string filepath) {
            //
            System.Console.WriteLine($"Loading file: {filepath}");
            var loaded = Image.Load(filepath);
            return loaded.CloneAs<Rgba32>();
        }

        public static void convertImage(Image<Rgba32> input) {
            System.Console.WriteLine($"Converting image of dimensions: {input.Width} by {input.Height}");
            var converted = new Image<Rgba32>(input.Width, input.Height);

            // // each pixel at once
            // for (int x = 0; x < input.Width; x++) {
            //     for (int y = 0; y < input.Height; y++) {
            //         var pixel = input.
            //     }
            // }
        }

        public static void saveImage(Image<Rgba32> image, string filepath) {
            System.Console.WriteLine($"Saving output to file path {filepath}");
            image.Save(filepath);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o => {
                    var img = loadImage(o.Filepath);
                    convertImage(img);
                    saveImage(img, o.OutputFilepath ?? $"{o.Filepath}-converted.png");
                });
        }
    }
}
