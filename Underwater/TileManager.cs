using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underwater
{
    class TileManager : IGameObject
    {
        #region variables

        GraphicsDeviceManager graphics;
        ContentManager content;
        Tile tileToInstance;
        List<Tile> tiles;
        

        #endregion

        public TileManager(GraphicsDeviceManager graphics, ContentManager content)
        {
            this.graphics = graphics;
            this.content = content;
        }

        private void createLevel()
        {
            string map = "4000655555555555555540006-4000723555555555551290006-4000006555555555554000006-4000006555555555554000006-4000007222222222229000006-4000000000000000000000006-4000000000000000000000006-4000000000000000000000006-4000000000000000000000006-4000000000000000000000006-7888888888300018888888889-5555555555400065555555555-5555555555400065555555555-5555555555400065555555555-5555555555400065555555555";
            string[] mapData = map.Split(new char[] { '-' });
            int mapXSize = mapData[0].ToCharArray().Length;
            int mapYSize = mapData.Length;

            for (int y = 0; y < mapYSize; y++)
            {//posições em y
                char[] tileType = mapData[y].ToCharArray();

                for (int x = 0; x < mapXSize; x++)
                {//posições em x
                        placeTile(tileType[x].ToString(), (x * 32), (y * 32));//hardcode temp
                }
            }
        }

        private void placeTile(string tileType, int x, int y)
        {
            int tileIndex = int.Parse(tileType);
            if (tileIndex != 0) {
                tileToInstance = new Tile(graphics, content, x, y, tileIndex);
                tileToInstance.initialize();
                tiles.Add(tileToInstance);
            }
        }

        public void initialize()
        {
            tiles = new List<Tile>();
            createLevel();
        }

        public void loadContent() { }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in tiles)
                tile.draw(spriteBatch);
        }

        public void update() { }

    }
}
