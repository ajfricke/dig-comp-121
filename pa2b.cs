using System;
using static System.Console;
using System.Drawing;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Bme121
{
    class Retina
    {
        public class Tile
        {
            // setting the length of the tile
            public const int Len = 8;

            // making property of array of colour type
            public Color[ , ] Pixels { get; set; }

            // making an 8x8 colour array representing pixels
            public Tile( ) { Pixels = new Color[ Len, Len ]; }
        }

        // making properties for an array of tile type, and for height and width
        public Tile[ , ] Tiles { get; set;}
        public int Height {get; private set;}
        public int Width {get; private set;}
       
       // making properties for the tile rows and columns
        public int tileRows { get; set; } 
        public int tileCols { get; set; }

        public Retina(string path)
        {
            // six labours - loading image
            Image< Rgba32 > img6L = Image.Load< Rgba32 >( path );

            // setting variables
            Height = img6L.Height;
            Width = img6L.Width;

            // calculating number of rows and columns of the picture
            tileRows = (int) Math.Ceiling( (double) Height / Tile.Len);
            tileCols = (int) Math.Ceiling( (double) Width  / Tile.Len);
            
            // making a 2D tile array
            Tiles = new Tile[ tileRows, tileCols];
            
            // tranversing through the tile array
            for( int tileCol = 0; tileCol < tileCols; tileCol ++)
            {
                for( int tileRow = 0; tileRow < tileRows; tileRow ++)
                {
                    // making a new tile at each spot in the tile array
                    Tiles[tileCol, tileRow] = new Tile();
                    
                    // transversing through each individual pixel
                    for( int row = Tile.Len*tileRow; row < Tile.Len*(tileRow+1); row ++ )
                    {
                        for ( int col = Tile.Len*tileCol; col < Tile.Len*(tileCol+1); col ++ )
                        {

                            // storing the pixel colours to use later
                            Rgba32 p = img6L[ row, col];
                            Color c = Color.FromArgb( p.A, p.R, p.G, p.B);

                            Tiles[ tileCol, tileRow ].Pixels[ col - Tile.Len*tileCol, row - Tile.Len*tileRow] = c;
                        }
                    }
                }
            }
        }

        public void SaveToFile(string path)
        {
            // six labours - making a new image
            Image< Rgba32 > img6L = new Image< Rgba32 >(Width, Height);
    
            // transversing throught the tiles
            for( int x = 0; x < tileRows; x ++)
            {
                for( int y = 0; y < tileCols; y ++)
                {
                    // transversing through the pixels
                    for( int row = Tile.Len*x; row < Tile.Len*(x+1); row ++ )
                    {
                        for ( int col = Tile.Len*y; col < Tile.Len*(y+1); col ++ )
                        {
                            // creating the new image by making the pixels the right colours
                            Color c = Tiles[y,x].Pixels[col - Tile.Len*y, row - Tile.Len*x];
                            Rgba32 p = new Rgba32(c.R, c.G, c.B, c.A);

                            img6L[ row, col ] = p;
                        }
                    }
                }
            }
    
            img6L.Save(path);
        }
    }
    static class Program
    {
        static void Main( )
        {
            const string path = "20051020_63711_0100_PP.png";
            
            Retina retina = new Retina( path );
            retina.SaveToFile( "test.png" );
        }
    }
}
