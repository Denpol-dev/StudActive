   M S B A M L   `   `   `  ????E  AStudActive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:StudActive
StudActive  5   	       StudActive.Login   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:StudActive     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	      MaterialDesignBody#???   5   	     MaterialDesignPaper#???  5   	     ? Title$  Вход??5   	   $	??450??6   $	??400??6#    ? WindowState$ Normal=?6/    ? WindowStyle$
 None=?6D    ? 
ResizeMode$ NoResize=?6W   .??5      ? .+?5   
   ? -   5         ?Name$ TopBar??5      $??35??6   $	??Top=?6+   ???#979797.+?5      ?? -   5         ??Name$ TopPanel??5       ?? Orientation$ 
Horizontal=?6+   $	??Top=?6D   $??Right=?6\   .+?5      ?? -   5         ??Name$ CollapseWin??5      $??25??6.   $??25??69   $??Right=?6E   $??5q?6a   
??? ???
??? ??? & "MaterialDesignFloatingActionButton#??[ 6?    ? Cursor$
 Hand??6?   .??5      ?? $8??2pack://application:,,,/Content/IconButtons/min.png??6   5      ?? 5      -   5        $ CloseWin??5      $??25??6+   $??25??66   $??Right=?6B   $??5q?6^   
???  ??
???  ??#??[ 6?   .??5      ?? $:??4pack://application:,,,/Content/IconButtons/close.png??6   5      5      5      ? 5   
   -   5        $ MainGrid??5      .+?5      ?? -   5        $ 
LoginPanel??5      $??Center=?6-   $ Vertical=?6H   $??Center=?6_   $y?Visible=?6|   .+?5      ?? $2??,pack://application:,,,/Content/susu_logo.png??6   $	??150??6N   ?? 5      -   5        	 ??Name$	 	LoginText??5      $??polurotovdv43??6-   $??	Comfortaa)?6B   
 ? ToolTip$
 Введите логин??6Y   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 
Логин??6q   $??Center=?6?   $??Center=?6?   $	??256??6?   $??10q?6?   $ Arrow??6?   G? 5      -   5         G?Name$ Password??5      $??	Comfortaa)?60   $!
 Введите пароль??6G   $ Пароль??6`   $??Center=?6?   $??Center=?6?   $	??256??6?   $??10q?6?   ?? 5      -	   5         ??Name$ 
ErrorLabel??5      $?? ??60   $??Center=?68   
???  ??$??	Comfortaa)?6f   $??Italic$?6}   $
??Wrap=?6?   $??	10,0,10,0q?6?    ?? TextAlignment$ Center=?6?   ?? 5      -
   5        $ LoginButton??5      $??
Войти??6.   $	??100??6>    ?? 	IsDefault$
 True??6J   $??	Comfortaa)?6[   $??5q?6r   6}   5      5      5       5!      