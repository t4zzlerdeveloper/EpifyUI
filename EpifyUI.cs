using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpifyUI
{
    public partial class Info
    {
        public readonly string Developer = "t4zzlerdeveloper";
        public readonly string GitHubRepository = "https://github.com/t4zzlerdeveloper/EpifyUI";
    }


    public static class Forms
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public static void EnableRoundedCorners(Form formToRound, int borderRadius = 15)
        {
            formToRound.FormBorderStyle = FormBorderStyle.None;
            formToRound.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, formToRound.Width, formToRound.Height, borderRadius, borderRadius));
        }


        public async static void FadeIn(Form formToFadeIn, int speed = 7)
        {
            formToFadeIn.Opacity = 0f;

            for (float f = 0f; f <= 1.05f; f += 0.05f)
            {
                await Task.Delay(speed);
                formToFadeIn.Opacity = f;
            }
        }

        public async static void FadeOut(Form formToFadeOut, int speed = 7, bool CloseAfterFade = false)
        {
            try
            {
                float f2 = float.Parse(formToFadeOut.Opacity.ToString());

                for (float f = f2; -0.05f <= f; f -= 0.05f)
                {
                    await Task.Delay(speed);
                    formToFadeOut.Opacity = f;
                }
            }
            catch { CloseAfterFade = true; }

            if (CloseAfterFade)
            {
                formToFadeOut.Close();
            }
        }

    }










    [
    Category("EpifyUI"),
    Description("Fully customisable gradient switch button.")
    ]
    public partial class gradientSwitchButton : UserControl
    {
        //BACK ON
        protected Color onBackColor1 = Color.MediumSlateBlue;
        protected Color onBackColor2 = Color.BlueViolet;

        public Color _onBackColor1 { get { return onBackColor1; } set { onBackColor1 = value; Invalidate(); } }
        public Color _onBackColor2 { get { return onBackColor2; } set { onBackColor2 = value; Invalidate(); } }

        protected int onBackColorAngle = 45;
        public int _onBackColorAngle { get { return onBackColorAngle; } set { onBackColorAngle = value; Invalidate(); } }


        //BACK OFF
        protected Color offBackColor1 = Color.Gray;
        protected Color offBackColor2 = Color.FromArgb(64,64,64);

        public Color _offBackColor1 { get { return offBackColor1; } set { offBackColor1 = value; Invalidate(); } }
        public Color _offBackColor2 { get { return offBackColor2; } set { offBackColor2 = value; Invalidate(); } }

        protected int offBackColorAngle = 45;
        public int _offBackColorAngle { get { return offBackColorAngle; } set { offBackColorAngle = value; Invalidate(); } }




        protected Color onSwitchColor = Color.WhiteSmoke;
        protected Color offSwitchColor = Color.WhiteSmoke;


        public Color _onSwitchColor { get { return onSwitchColor; } set { onSwitchColor = value; Invalidate(); } }
        public Color _offSwitchColor { get { return offSwitchColor; } set { offSwitchColor = value; Invalidate(); } }




        protected bool chd = false;

        public bool _Switched { get { return chd; } set { chd = value; Invalidate(); } }


        public gradientSwitchButton()
        {
            MinimumSize = new Size(45, 22);
            Size = new Size(75, 40);
            BackColor = Color.Transparent;
        }


        private GraphicsPath GFP()
        {
            int arcSize = Height - 1;
            Rectangle leftArc = new Rectangle(0, 0, arcSize, arcSize);
            Rectangle rightArc = new Rectangle(Width - arcSize - 2, 0, arcSize, arcSize);

            GraphicsPath gp = new GraphicsPath();
            gp.StartFigure();
            gp.AddArc(leftArc,90,180);
            gp.AddArc(rightArc, 270, 180);
            gp.CloseFigure();

            return gp;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (Height >= Width)
            {
                Width = Height * 2;
            }

           //base.OnSizeChanged(e);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            int toggleSize = Height - 5;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            if (_Switched)
            {
                //BACK ON
                e.Graphics.FillPath(new LinearGradientBrush(ClientRectangle, onBackColor1, onBackColor2, onBackColorAngle), GFP());

                //SWITCH ON
                e.Graphics.FillEllipse(new SolidBrush(onSwitchColor),new Rectangle(Width-Height+1,2,toggleSize,toggleSize));
            }
            else
            {
                //BACK OFF
                e.Graphics.FillPath(new LinearGradientBrush(ClientRectangle, offBackColor1, offBackColor2, offBackColorAngle), GFP());

                //SWITCH OFF
                e.Graphics.FillEllipse(new SolidBrush(offSwitchColor), new Rectangle(2, 2, toggleSize, toggleSize));
            }

            base.OnPaint(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (_Switched)
            {
                _Switched = false;
            }
            else
            {
                _Switched = true;
            }
            Invalidate();
            base.OnClick(e);
        }

    }





    [
    Category("EpifyUI"),
    Description("Fully customisable gradient menu.")
    ]
    public partial class gradientMenu : Panel
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();




        protected Color c1 = Color.FromArgb(128, 255, 128);
        protected Color c2 = Color.DodgerBlue;

        public Color _Color1 { get { return c1; } set { c1 = value; Invalidate(); } }
        public Color _Color2 { get { return c2; } set { c2 = value; Invalidate(); } }



        protected bool anim = false;

        public bool _Animated
        {
            get { return anim; }
            set
            {
                anim = value;

                if (anim && !tt.Enabled)
                {
                    tt.Start();
                    tt.Tick += (ss, ee) => {
                        if (c1 == c2)
                        {
                            _Animated = false;
                        }
                        _Angle = ang % 360 + 1;
                    };
                }
                else
                {
                    tt.Stop();
                }

                Invalidate();
            }
        }

        public int _AnimationSpeed { get { return tt.Interval; } set { tt.Interval = value; Invalidate(); } }


        protected float ang = 45;

        public float _Angle { get { return ang; } set { ang = value; Invalidate(); } }


        protected System.Windows.Forms.Timer tt = new System.Windows.Forms.Timer();


        protected bool aoh = false;

        public bool _AnimateOnHover { get { return aoh; } set { aoh = value; Invalidate(); } }


        protected bool mf = false;

        public bool _MoveForm { get { return mf; } set { mf = value; Invalidate(); } }




        //Menu Stuff
        protected int MinWidth = 65;
        protected int MaxWidth = 215;

        public int _MinWidth { get { return MinWidth; } set { MinWidth = value; Invalidate(); } }
        public int _MaxWidth { get { return MaxWidth; } set { MaxWidth = value; Invalidate(); } }



        protected bool menuopened = false;
        protected bool blockmenu= false;

        public bool _MenuOpened { get { return menuopened; } }
        public bool _BlockMenu { get { return blockmenu; } set { blockmenu = value; Invalidate(); } }


        protected System.Windows.Forms.Timer timerOpenPanelMenu = new System.Windows.Forms.Timer();
        protected System.Windows.Forms.Timer timerClosePanelMenu = new System.Windows.Forms.Timer();


        protected BackgroundWorker work = new BackgroundWorker();



        public gradientMenu()
        {
            Size = new Size(215, 500);
            DoubleBuffered = true;

            tt.Interval = 60;


            timerOpenPanelMenu.Interval = 6;
            timerClosePanelMenu.Interval = 6;

            timerOpenPanelMenu.Tick += (sender, e) => timerOpenPanelMenu_Tick(sender, e, this);
            timerClosePanelMenu.Tick += (sender, e) => timerClosePanelMenu_Tick(sender, e, this);

            HandleCreated += new EventHandler((sender, args) =>
            {
                work.DoWork += (obj, e) => CheckLoc(this);
                work.RunWorkerAsync();
            });

            BringToFront();

        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            e.Control.BackColor = Color.Transparent;
            base.OnControlAdded(e);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_MoveForm)
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    IntPtr handle = GetForegroundWindow();
                    SendMessage(handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            base.OnMouseDown(e);
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            GraphicsPath gp = new GraphicsPath();

            gp.AddRectangle(new RectangleF(new Point(0, 0), new Size(Width, Height)));

            e.Graphics.FillPath(new LinearGradientBrush(ClientRectangle, c1, c2, ang), gp);

            base.OnPaint(e);
        }


        private void CheckLoc(Control pan)
        {
            while (true)
            {
                Thread.Sleep(60);
                Point pos = new Point(0,0);
                pan.Invoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    pos = pan.PointToClient(Cursor.Position);

                if (pos.X > MaxWidth || pos.X < 0 || pos.Y > Height || pos.Y < 0 && menuopened)
                {
                    timerOpenPanelMenu.Stop();
                    timerClosePanelMenu.Start();
                }
                else if (pos.X < MinWidth  && pos.Y > 0)
                {
                    timerClosePanelMenu.Stop();
                    timerOpenPanelMenu.Start();
                }
                });
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (_AnimateOnHover)
            {
                _Animated = true;
            }
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            if (_AnimateOnHover)
            {
                _Animated = false;
            }
        }



        private void timerOpenPanelMenu_Tick(object sender, EventArgs e,Control pan)
        {
            pan.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
            if (!blockmenu)
            {
                if (pan.Width < MaxWidth && !timerClosePanelMenu.Enabled)
                {
                    pan.Size = new Size(pan.Width + 15, pan.Height);
                    pan.Invalidate();
                }
                else
                {
                    pan.Size = new Size(MaxWidth, pan.Height);
                    pan.Invalidate();
                    menuopened = true;
                    timerOpenPanelMenu.Stop();
                }
            }
                //else
                //{
                //    Size = new Size(MinWidth, Height);
                //    Invalidate();
                //    menuopened = false;
                //    timerOpenPanelMenu.Stop();
                //}
            });
        }

        private void timerClosePanelMenu_Tick(object sender, EventArgs e, Control pan)
        {
            pan.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                if (!blockmenu)
                {
                    if (pan.Width > MinWidth)
                    {
                        pan.Size = new Size(pan.Width - 15, pan.Height);
                        pan.Invalidate();
                    }
                    else
                    {
                        pan.Size = new Size(MinWidth, pan.Height);
                        pan.Invalidate();
                        menuopened = false;
                        timerClosePanelMenu.Stop();
                    }
                }
                //else
                //{
                //    Size = new Size(MinWidth, Height);
                //    Invalidate();
                //    menuopened = false;
                //    timerClosePanelMenu.Stop();
                //}
            });
        }


    }






    [
    Category("EpifyUI"),
    Description("Fully customisable gradient panel.")
    ]
    public partial class gradientPanel : Panel
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();




        protected Color c1 = Color.FromArgb(128, 255, 128);
        protected Color c2 = Color.DodgerBlue;

        public Color _Color1 { get { return c1; } set { c1 = value; Invalidate(); } }
        public Color _Color2 { get { return c2; } set { c2 = value; Invalidate(); } }


        protected int wh = 45;

        public int _BorderRadius { get { return wh; } set { wh = value; Invalidate(); } }



        protected bool anim = false;

        public bool _Animated
        {
            get { return anim; }
            set
            {
                anim = value;

                if (anim && !tt.Enabled)
                {
                    tt.Start();
                    tt.Tick += (ss, ee) => {
                        if (c1 == c2)
                        {
                            _Animated = false;
                        }
                        _Angle = ang % 360 + 1;
                    };
                }
                else
                {
                    tt.Stop();
                }

                Invalidate();
            }
        }

        public int _AnimationSpeed { get { return tt.Interval; } set { tt.Interval = value; Invalidate(); } }


        protected float ang = 45;

        public float _Angle { get { return ang; } set { ang = value; Invalidate(); } }


        protected System.Windows.Forms.Timer tt = new System.Windows.Forms.Timer();



        protected bool fc = false;

        public bool _ForceCircle { get { return fc; } set { fc = value; OnSizeChanged(new EventArgs()); Invalidate(); } }


        protected bool fpc = false;

        public bool _ForcePerfectCorners { get { return fpc; } set { fpc = value; OnSizeChanged(new EventArgs()); Invalidate(); } }



        protected bool aoh = false;

        public bool _AnimateOnHover { get { return aoh; } set { aoh = value; Invalidate(); } }


        protected bool mf = false;

        public bool _MoveForm { get { return mf; } set { mf = value; Invalidate(); } }


        public gradientPanel()
        {
            Size = new Size(200, 100);
            DoubleBuffered = true;

            tt.Interval = 60;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            e.Control.BackColor = Color.Transparent;
            base.OnControlAdded(e);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_MoveForm)
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    IntPtr handle = GetForegroundWindow();
                    SendMessage(handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            base.OnMouseDown(e);
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            if (_ForceCircle)
            {
                Width = Height;
                _BorderRadius = Width;
            }
            else if (_ForcePerfectCorners)
            {
                _BorderRadius = Height;
                if (Width < Height)
                {
                    Height = Width;
                }
            }

            base.OnSizeChanged(e);
        }


        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            GraphicsPath gp = new GraphicsPath();

            if (wh != 0)
            {
                gp.AddArc(new Rectangle(0, 0, wh, wh), 180, 90);
                gp.AddArc(new Rectangle(Width - wh - 1, 0, wh, wh), -90, 90);
                gp.AddArc(new Rectangle(Width - wh - 1, Height - wh - 1, wh, wh), 0, 90);
                gp.AddArc(new Rectangle(0, Height - wh - 1, wh, wh), 90, 90);
            }
            else
            {
                gp.AddRectangle(new RectangleF(new Point(0, 0), new Size(Width, Height)));
            }

            e.Graphics.FillPath(new LinearGradientBrush(ClientRectangle, c1, c2, ang), gp);

            base.OnPaint(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (_AnimateOnHover)
            {
                _Animated = true;
            }
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            if (_AnimateOnHover)
            {
                _Animated = false;
            }
        }

    }


    [
    Category("EpifyUI"),
    Description("Fully customisable gradient button."),
    DefaultEvent("Click")
    ]
    public partial class gradientButton : UserControl
    {
        protected Color c1 = Color.FromArgb(192, 0, 192);
        protected Color c2 = Color.Red;

        public Color _Color1 { get { return c1; } set { c1 = value; 
                if(_SmartColor)
                {
                    _HoverColor1 = _Color1;
                    try
                    {
                        if (_HoverColor1.R - 10 >= 0)
                        {
                            _HoverColor1 = Color.FromArgb(_HoverColor1.R - 10, _HoverColor1.G, _HoverColor1.B);
                        }
                    }
                    catch { }
                    try
                    {
                        if (_HoverColor1.G - 10 >= 0)
                        {
                            _HoverColor1 = Color.FromArgb(_HoverColor1.R, _HoverColor1.G - 10, _HoverColor1.B);
                        }
                    }
                    catch { }
                    try
                    {
                        if (_HoverColor1.B - 10 >= 0)
                        {
                            _HoverColor1 = Color.FromArgb(_HoverColor1.R, _HoverColor1.G, _HoverColor1.B - 10);
                        }
                    }
                    catch { }
                }
                Invalidate();
            } }
        public Color _Color2 { get { return c2; } set { c2 = value;
                if (_SmartColor)
                {
                    _HoverColor2 = _Color2;
                    try
                    {
                        if (_HoverColor2.R - 20 >= 0)
                        {
                            _HoverColor2 = Color.FromArgb(_HoverColor2.R - 20, _HoverColor2.G, _HoverColor2.B);
                        }
                    }
                    catch { }
                    try
                    {
                        if (_HoverColor2.G - 20 >= 0)
                        {
                            _HoverColor2 = Color.FromArgb(_HoverColor2.R, _HoverColor2.G - 20, _HoverColor2.B);
                        }
                    }
                    catch { }
                    try
                    {
                        if (_HoverColor2.B - 20 >= 0)
                        {
                            _HoverColor2 = Color.FromArgb(_HoverColor2.R, _HoverColor2.G, _HoverColor2.B - 20);
                        }
                    }
                    catch { }
                }
                Invalidate(); 
            } }



        protected Color nc = Color.WhiteSmoke;
        protected Color hfc = Color.DarkGray;

        public Color _HoverForeColor { get { return hfc; } set { hfc = value; Invalidate(); } }

        protected Color hc1 = Color.FromArgb(192, 0, 192);
        protected Color hc2 = Color.Red;

        public Color _HoverColor1 { get { return hc1; } set { hc1 = value; Invalidate(); } }
        public Color _HoverColor2 { get { return hc2; } set { hc2 = value; Invalidate(); } }

        protected bool _IsMouseHover = false;

        protected bool sc = false;

        public bool _SmartColor { get { return sc; } set { sc = value;
                Color ctemp = _Color1;
                _Color1 = Color.Black;
                _Color1 = ctemp;
                ctemp = _Color2;
                _Color2 = Color.Black;
                _Color2 = ctemp;

                Invalidate(); 
            } }

        protected bool ehc = true;

        public bool _EnableHoverColors { get { return ehc; } set { ehc = value; Invalidate(); } }


        protected int wh = 45;

        public int _BorderRadius { get { return wh; } set { wh = value; Invalidate(); } }



        protected bool anim = false;

        public bool _Animated { get { return anim; } set { anim = value;

                if (anim && !tt.Enabled)
                {
                    tt.Start();
                    tt.Tick += (ss, ee) => {
                        if (c1 == c2)
                        {
                            _Animated = false;
                        }
                        _Angle = ang % 360 + 1; };
                }
                else
                {
                    tt.Stop();
                }
                
                Invalidate(); 
            } }

        public int _AnimationSpeed { get { return tt.Interval; } set { tt.Interval = value; Invalidate(); } }


        protected float ang = 45;

        public float _Angle { get { return ang; } set { ang = value; Invalidate(); } }


        protected System.Windows.Forms.Timer tt = new System.Windows.Forms.Timer();



        protected string txt = "Button";

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text { get { return txt; } set { txt = value; Invalidate(); } }


        protected bool fc = false;

        public bool _ForceCircle { get { return fc; } set { fc = value; OnSizeChanged(new EventArgs()); Invalidate(); } }


        protected bool fpc = false;

        public bool _ForcePerfectCorners { get { return fpc; } set { fpc = value; OnSizeChanged(new EventArgs()); Invalidate(); } }


        protected bool aoh = false;

        public bool _AnimateOnHover { get { return aoh; } set { aoh = value; Invalidate(); } }

        protected bool ftl = false;

        public bool _FlatTopLeft { get { return ftl; } set { ftl = value; Invalidate(); } }


        public gradientButton()
        {
            hc1 = c1;
            hc2 = c2;
            ForeColor = nc;
            Size = new Size(160, 45);
            Font = new Font("Segoe UI", 12, FontStyle.Bold);
            DoubleBuffered = true;

            tt.Interval = 60;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (_ForceCircle)
            {
                Width = Height;
                _BorderRadius = Width;
            }
            else if(_ForcePerfectCorners)
            {
                _BorderRadius = Height;
                if (Width < Height)
                {
                    Height = Width;
                }
            }

            base.OnSizeChanged(e);
        }


        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            GraphicsPath gp = new GraphicsPath();

            if (wh != 0)
            {
                int wh2 = 1;
                if (!_FlatTopLeft)
                {
                    wh2 = wh;
                }
                gp.AddArc(new Rectangle(0, 0, wh2, wh2), 180, 90);
                gp.AddArc(new Rectangle(Width - wh -1, 0, wh, wh), -90, 90);
                gp.AddArc(new Rectangle(Width - wh -1, Height - wh - 1, wh, wh), 0, 90);
                gp.AddArc(new Rectangle(0, Height - wh -1, wh, wh), 90, 90);   
            }
            else
            {
                gp.AddRectangle(new RectangleF(new Point(0,0),new Size(Width,Height)));
            }

            if (_IsMouseHover && _EnableHoverColors)
            {
                e.Graphics.FillPath(new LinearGradientBrush(ClientRectangle, hc1, hc2, ang), gp);
            }
            else
            {
                e.Graphics.FillPath(new LinearGradientBrush(ClientRectangle, c1, c2, ang), gp);
            }



            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(e.Graphics, txt, Font, new Point(Width + 3, Height / 2), ForeColor, flags);


            base.OnPaint(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _IsMouseHover = true;
            nc = ForeColor;
            ForeColor = hfc;
            if (_AnimateOnHover)
            {
                _Animated = true;
            }
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            _IsMouseHover = false;
            ForeColor = nc;
            if (_AnimateOnHover)
            {
                _Animated = false;
            }
        }
    }




    [
    Category("EpifyUI"),
    Description("Adjustable chroma button."),
    DefaultEvent("Click")
    ]
    public partial class chromaButton : UserControl
    {
        protected Color c1 = Color.Red;
        protected Color c2 = Color.FromArgb(255, 128, 0);

        protected Color nc = Color.WhiteSmoke;
        protected Color hc = Color.DarkGray;

        public Color _HoverColor { get { return hc; } set { hc = value; Invalidate(); } }


        protected int wh = 45;

        public int _BorderRadius { get { return wh; } set { wh = value; Invalidate(); } }


        public int _ChromaSpeed { get { return tt.Interval; } set { tt.Interval = value; Invalidate(); } }


        protected float ang = 45;

        public float _Angle { get { return ang; } set { ang = value; Invalidate(); } }


        protected System.Windows.Forms.Timer tt = new System.Windows.Forms.Timer();


        protected string txt = "Button";

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text { get { return txt; } set { txt = value; Invalidate(); } }


        protected int k = 0;
        protected Random rnd = new Random();
        protected double i = 0d;


        public chromaButton()
        {
            ForeColor = nc;
            Size = new Size(160, 45);
            Font = new Font("Segoe UI", 12, FontStyle.Bold);
            DoubleBuffered = true;

            tt.Interval = 50;
            tt.Start();
            tt.Tick += (ss, ee) => {
                try
                {
                    if(i< 32d)
                    {
                        i++;
                        double frequency = 0.3d;
                        int red = int.Parse(Math.Round((Math.Sin(frequency * i + 0d) * 127d + 128d)).ToString());
                        int green = int.Parse(Math.Round((Math.Sin(frequency * i + 2d) * 127d + 128d)).ToString());
                        int blue = int.Parse(Math.Round((Math.Sin(frequency * i + 4d) * 127d + 128d)).ToString());

                        c1 = Color.FromArgb(red, green, blue);

                        frequency = 0.363d;
                        red = int.Parse(Math.Round((Math.Sin(frequency * i + 0d) * 127d + 128d)).ToString());
                        green = int.Parse(Math.Round((Math.Sin(frequency * i + 2d) * 127d + 128d)).ToString());
                        blue = int.Parse(Math.Round((Math.Sin(frequency * i + 4d) * 127d + 128d)).ToString());

                        c2 = Color.FromArgb(red, green, blue);
                    }
                    else
                    {
                        i=0;
                    }
                }
                catch { }         

                Invalidate();
            };
        }


        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            GraphicsPath gp = new GraphicsPath();

            if (wh != 0)
            {
                gp.AddArc(new Rectangle(0, 0, wh, wh), 180, 90);
                gp.AddArc(new Rectangle(Width - wh - 1, 0, wh, wh), -90, 90);
                gp.AddArc(new Rectangle(Width - wh - 1, Height - wh - 1, wh, wh), 0, 90);
                gp.AddArc(new Rectangle(0, Height - wh - 1, wh, wh), 90, 90);
            }
            else
            {
                gp.AddRectangle(new RectangleF(new Point(0, 0), new Size(Width, Height)));
            }

            e.Graphics.FillPath(new LinearGradientBrush(ClientRectangle, c1, c2, ang), gp);

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(e.Graphics, txt, Font, new Point(Width + 3, Height / 2), ForeColor, flags);

            base.OnPaint(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            nc = ForeColor;
            ForeColor = hc;
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            ForeColor = nc;
        }
    }



    [
    Category("EpifyUI"),
    Description("Fully customisable gradient label."),
    DefaultEvent("Click")
    ]
    public partial class gradientLabel : UserControl
    {
        protected Color c1 = Color.FromArgb(192, 0, 192);
        protected Color c2 = Color.Red;

        public Color _Color1 { get { return c1; } set { c1 = value; Invalidate(); } }
        public Color _Color2 { get { return c2; } set { c2 = value; Invalidate(); } }



        protected bool anim = false;

        public bool _Animated
        {
            get { return anim; }
            set
            {
                anim = value;

                if (anim && !tt.Enabled)
                {
                    tt.Start();
                    tt.Tick += (ss, ee) => {
                        if (c1 == c2)
                        {
                            _Animated = false;
                        }
                        _Angle = ang % 360 + 1;
                    };
                }
                else
                {
                    tt.Stop();
                }

                Invalidate();
            }
        }

        public int _AnimationSpeed { get { return tt.Interval; } set { tt.Interval = value; Invalidate(); } }


        protected float ang = 45;

        public float _Angle { get { return ang; } set { ang = value; Invalidate(); } }


        protected System.Windows.Forms.Timer tt = new System.Windows.Forms.Timer();



        protected string txt = "Button";

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text { get { return txt; } set { txt = value; OnSizeChanged(new EventArgs()); Invalidate(); } }


        protected bool aoh = false;

        public bool _AnimateOnHover { get { return aoh; } set { aoh = value; Invalidate(); } }


        public gradientLabel()
        {
            Size = new Size(160, 45);
            Font = new Font("Segoe UI", 15, FontStyle.Regular);
            DoubleBuffered = true;

            tt.Interval = 60;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            Size = getTextSize();
            base.OnFontChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Size =  getTextSize();
            base.OnSizeChanged(e);
        }

        private Size getTextSize()
        {
            Image fakeImage = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(fakeImage);
            SizeF sizef = graphics.MeasureString(txt, Font);
            int w = int.Parse(Math.Round(double.Parse(sizef.Width.ToString())).ToString());
            int h = int.Parse(Math.Round(double.Parse(sizef.Height.ToString())).ToString());
            return new Size(w,h);
        }


        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0 , 0, Width, Height), c1,c2,ang);

            e.Graphics.DrawString(txt,Font,brush,0,0);

            base.OnPaint(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (_AnimateOnHover)
            {
                _Animated = true;
            }
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            if (_AnimateOnHover)
            {
                _Animated = false;
            }
        }
    }


}
