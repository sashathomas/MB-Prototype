using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.ImGuiTools;

namespace NezTest
{
    public class Game1 : Core
    {


        public Game1() : base(640, 360, false, "NezTest", "Content")
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            Scene = new MasterScene();
            
            //var imGuiManager = new ImGuiManager();
            //Core.RegisterGlobalManager(imGuiManager); 
            
        }
    }
}
