﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed_Side_scrolling
{
    public class GameScene : asd.Scene
    {
        protected override void OnStart()
        {
            asd.Layer2D Lgame = new asd.Layer2D();
            Maps map = MapManager.Read("Maps/01.txt");

            Lgame.AddObject(map);


            Player player = new Player(map);
            FlyingEnemy heli = new FlyingEnemy(asd.Engine.Graphics.CreateTexture2D("Resources/Characters/heli.png"), player, map);
            Lgame.DrawingPriority = 2;
            Lgame.AddObject(player);
            Lgame.AddObject(heli);
            foreach(Enemy e in map.Enemies)
            {
                Lgame.AddObject(e);
            }
            this.AddLayer(Lgame);

            asd.Layer2D Lback = new asd.Layer2D();
            Background Gbacks = new Background(map.Length * 32);
            Lback.DrawingPriority = 0;
            Lback.AddObject(Gbacks);
            this.AddLayer(Lback);


            asd.Layer2D Lui = new asd.Layer2D();
            Lui.DrawingPriority = 3;
            this.AddLayer(Lui);
            FPSViewer fps = new FPSViewer();
            Lui.AddObject(fps);
            TimeCounter tc = new TimeCounter();
            Lui.AddObject(tc);
            tc.Start();

            Camera Cam;
            Cam = new Camera(player);
            Lgame.AddObject(Cam);
            BackgroundCamera BCam;
            BCam = new BackgroundCamera(player);
            Lback.AddObject(BCam);

            Sound.BGMStart();
        }
    }

    public class DeadScene : asd.Scene
    {
        protected override void OnStart()
        {
            asd.Layer2D Lback = new asd.Layer2D();
            Lback.DrawingPriority = 0;
            Background Gback = new Background(0);
            Lback.AddObject(Gback);
            asd.CameraObject2D Camera = new asd.CameraObject2D();
            Camera.Dst = new asd.RectI(0, 0, 960, 640);
            Camera.Src = new asd.RectI(0, 0, 480, 320);
            Lback.AddObject(Camera);

            asd.Layer2D Ldead = new asd.Layer2D();
            Ldead.DrawingPriority = 1;
            asd.TextObject2D Tdead = new asd.TextObject2D();
            Tdead.Font = FontContainer.PMP12_60B;
            Tdead.Text = "突然の死";
            asd.Vector2DI fsize = FontContainer.PMP12_60B.CalcTextureSize("突然の死", asd.WritingDirection.Horizontal);
            Tdead.CenterPosition = new asd.Vector2DF(fsize.X, fsize.Y) / 2;
            Tdead.Position = new asd.Vector2DF(asd.Engine.WindowSize.X, asd.Engine.WindowSize.Y) / 2;
            Ldead.AddObject(Tdead);

            BlinkingText Tpzkts = new BlinkingText(30);
            Tpzkts.Text = "RETRY Z KEY TO START!";
            Tpzkts.Font = FontContainer.PMP10_30B;
            fsize = FontContainer.PMP10_30B.CalcTextureSize("RETRY Z KEY TO START!", asd.WritingDirection.Horizontal);
            Tpzkts.CenterPosition = new asd.Vector2DF(fsize.X, fsize.Y) / 2;
            Tpzkts.Position = new asd.Vector2DF(asd.Engine.WindowSize.X / 2, 384.0f);
            Ldead.AddObject(Tpzkts);

            this.AddLayer(Ldead);
            this.AddLayer(Lback);

            Sound.BGMStop();
            System.Console.WriteLine("突然の死");
        }
        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push) asd.Engine.ChangeScene(new GameScene());
        }
    }

    public class TitleScene : asd.Scene
    {
        protected override void OnStart()
        {
            asd.Layer2D Lback = new asd.Layer2D();
            Lback.DrawingPriority = 0;
            Background Gback = new Background(0);
            Lback.AddObject(Gback);
            asd.CameraObject2D Camera = new asd.CameraObject2D();
            Camera.Dst = new asd.RectI(0, 0, 960, 640);
            Camera.Src = new asd.RectI(0, 0, 480, 320);
            Lback.AddObject(Camera);

            asd.Layer2D Ltitle = new asd.Layer2D();
            Ltitle.DrawingPriority = 1;
            asd.TextObject2D TTitle = new asd.TextObject2D();
            TTitle.Text = Altseed_Side_scrolling_Core.Title;
            TTitle.Font = FontContainer.PMP12_60B;
            asd.Vector2DI fsize = FontContainer.PMP12_60B.CalcTextureSize(Altseed_Side_scrolling_Core.Title, asd.WritingDirection.Horizontal);
            TTitle.CenterPosition = new asd.Vector2DF(fsize.X, fsize.Y) / 2;
            TTitle.Position = new asd.Vector2DF(asd.Engine.WindowSize.X / 2, 128.0f);

            BlinkingText Tpzkts = new BlinkingText(30);
            Tpzkts.Text = "PRESS Z KEY TO START!";
            Tpzkts.Font = FontContainer.PMP10_30B;
            fsize = FontContainer.PMP10_30B.CalcTextureSize("PRESS Z KEY TO START!", asd.WritingDirection.Horizontal);
            Tpzkts.CenterPosition = new asd.Vector2DF(fsize.X, fsize.Y) / 2;
            Tpzkts.Position = new asd.Vector2DF(asd.Engine.WindowSize.X / 2, 384.0f);

            Ltitle.AddObject(Tpzkts);
            Ltitle.AddObject(TTitle);
            this.AddLayer(Ltitle);
            this.AddLayer(Lback);

        }

        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push) asd.Engine.ChangeScene(new GameScene());

        }
    }
}
