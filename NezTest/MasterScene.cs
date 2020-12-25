using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Tiled;
using Nez.AI.FSM;
using Nez.Sprites;

namespace NezTest
{
    public class MasterScene : Scene
    {
        
        public override void Initialize()
        {
            SetDesignResolution(1280, 720, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(1280, 720);
            ClearColor = Color.SkyBlue;
            AddRenderer(new DefaultRenderer());
            
            
            
            var tileMap = Content.LoadTiledMap("Content/tile2.tmx");
            var tileEntity = CreateEntity("tilemap");
            var objectLayer = tileMap.GetObjectGroup("objects");
            var spawn = objectLayer.Objects["spawn"];
            //var vinSprite = Content.LoadTexture("Content/Adventurer-1.5/Individual Sprites/adventurer-idle-00.png");

            var objectGroup = tileMap.GetObjectGroup("objects");
            var metalSprite = Content.LoadTexture("Content/metal1.png");
            var metal = CreateEntity("metal");

            var atlas = Content.LoadSpriteAtlas("Content/out.atlas");
            var tiledMapRenderer = tileEntity.AddComponent(new TiledMapRenderer(tileMap, "collision"));

            //tiledMapRenderer.SetLayersToRender(new[] { "tiles", "terrain", "details" });

            //tiledMapRenderer.RenderLayer = 1;
            var tiledMapDetailsComp = tileEntity.AddComponent(new TiledMapRenderer(tileMap));
            //tiledMapDetailsComp.SetLayerToRender("above-tiles");
            //tiledMapDetailsComp.RenderLayer = 1;
            var player = CreateEntity("player");
            metal.AddComponent(new Metal(this));
            metal.Transform.SetPosition(new Vector2(200, 600));
            var metalRenderer = metal.AddComponent(new TiledSpriteRenderer(metalSprite));


            var metalCollider = metal.AddComponent(new BoxCollider(-16, -16, 32, 32));
            metal.AddComponent(new TiledMapMover(tileMap.GetLayer<TmxLayer>("collision")));
            
            
            player.AddComponent(new Player(this));
            player.Transform.SetPosition(new Vector2(300, 400));
            //player.AddComponent(new TiledSpriteRenderer(vinSprite));
            //var renderer = player.AddComponent(new SpriteAnimator()).AddAnimationsFromAtlas(atlas);

            //player.AddComponent(new TmxAnimationFrame());
            //renderer.SetRenderLayer(5);
            var collider = player.AddComponent(new BoxCollider(-8, -16, 16, 32));
            player.AddComponent(new TiledMapMover(tileMap.GetLayer<TmxLayer>("collision")));
            //player.AddComponent(new TiledMapMover(tileMap.GetLayer<TmxLayer>("sub")));
            //metalRenderer.SetRenderLayer(3);
            //Flags.SetFlagExclusive(ref collider.CollidesWithLayers, 0);
            //Flags.SetFlagExclusive(ref collider.CollidesWithLayers, 1);



            Camera.Entity.AddComponent(new FollowCamera(player, FollowCamera.CameraStyle.LockOn));

        }

        public override void OnStart()
        { 
            //Camera.SetZoom(0.2f);
        }

    }
}
