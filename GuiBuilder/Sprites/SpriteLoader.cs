using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace GuiBuilder.Sprites
{
    public static class SpriteLoader
    {
        private static Dictionary<String, Texture> textures = new Dictionary<String, Texture>();

        public static void LoadTextures(OpenGL gl, string name, string path)
        {
            if (textures.ContainsKey(name))
                return;
            textures.Add(name, new Texture());
            textures[name].Create(gl, path);
        }

        public static void Bind(OpenGL gl, String tex)
        {
            if (textures.ContainsKey(tex))
                textures[tex].Bind(gl);
        }
    }
}
