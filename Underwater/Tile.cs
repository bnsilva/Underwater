using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Underwater
{
    class Tile : IGameObject
    {
        #region variables

        GraphicsDeviceManager graphics;
        ContentManager content;
        Texture2D texture;
        Rectangle sourceRectangle;
        Vector2 position = Vector2.Zero;
        float layerDepth;
        const int TILESIZE = 32;

        #endregion

        public void getTileset(int tileIndex)
        {
            int x = 0, y = 0;
            if(tileIndex > 6)
            {
                y = 2;
            } else if (tileIndex > 3)
            {
                y = 1;
            }
            switch (tileIndex % 3)
            {
                case 0:
                    x = 2;
                    break;
                case 1:
                    x = 0;
                    break;
                case 2:
                    x = 1;
                    break;
            }
            sourceRectangle = new Rectangle(TILESIZE*x, TILESIZE*y, TILESIZE, TILESIZE);
        }

        public Tile(GraphicsDeviceManager graphics, ContentManager content, int x, int y, int tileIndex)
        {
            this.graphics = graphics;
            this.content = content;
            getTileset(tileIndex);
            position = new Vector2(x,y);
        }

        public void initialize()
        {
            layerDepth = 1f;
        }

        public void loadContent()
        {
            texture = content.Load<Texture2D>("underwaterTileset");
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                content.Load<Texture2D>("underwaterTileset"),
                position,
                sourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                layerDepth
                );
        }

        public void update() { }

    }
}
