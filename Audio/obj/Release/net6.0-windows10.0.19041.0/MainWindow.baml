   M S B A M L   `   `   `  ���� @  <Audio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AudioAudio      Audio.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:Audio     -     � Title$  
Плеер��$	��469��$	��369��
������  � 
ResizeMode$ CanMinimize=� � WindowStyle$
 None=� � AllowsTransparency$
 True��
������  � Icon$ /Images/Icon.jpg��.��� .+��� -    ��Name$ BackgroundImage��$��/Images/LoadImage.png��$
��Fill=� J� Clip ��  �� RadiusX$ 35Q� �� RadiusY$ 35Q�	 �� Rect$	 0,0,370,470�
 J� Effect
 -  'System.Windows.Media.Effects.BlurEffect     Radius$ 20Q���  �� CornerRadius$ 30�����#a000d6$��2q��� -   $ 
SoundImage��$	��304��$��32,66,33,187q�$��Uniform=� �� BitmapScalingMode$ HighQuality=��� -    ��Name$ SoundArtist��$��0,51,0,0q�
�������$��Arial Black)�$
��Bold"�$	��Top=�$��Center=� �� TextAlignment$ Center=�$	��266��$��21��$
��Wrap=�$��CharacterEllipsis=��� -   $ StartPositionText��$��
10,313,0,0q�
�������$��Arial Black)�$
��Bold"�$	��Top=�$
��Left=�$ Center=�$��38��$��21��$
��Wrap=�$��CharacterEllipsis=��� -   $ EndPositionText��$��316,316,0,0q�
�������$��Arial Black)�$
��Bold"�$	��Top=�$
��Left=�$ Center=�$��38��$��21��$
��Wrap=�$��CharacterEllipsis=��� -   $ 	SoundName��$��0,284,0,153q�
�������$��Arial Black)�$��Center=�$��16(�$
��Wrap=��� -   $ VolumeLevel��$��335,414,0,42q�
�������$��Arial Black)�$
��Left=�$��12(�$
��Wrap=��� -	    ��Name$ ToFix��
������ $��26,21,299,419q�
������ .���� -
   $ FixImage��$��/Images/FixOff.png�� � Cursor$
 Hand������  �� 
TargetType" ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R��� #��z��#��z��._��� $��Center=�$��Center=��� -   $ StartAndStopButton��
������ $��166,374,166,59q�
������ .���� -   $ StartAndStop��$��Images/Start.png��$
 Hand������ " ��.���  � �� �� " ��.R��� #��z��#��z��._��� $��Center=�$��Center=��� -   $ button��$��X��
���  ��$��20(�$��	0,21,19,0q�
������ ���	#FF262626$��Center=�$��Center=�$��Arial Rounded MT Bold)�$
��Bold"�$��29��$��29��$	��Top=�$��Right=� J� RenderTransformOrigin$ 0.689,0.552&�$
 Hand������ " ��.���  � �� �� " ��.R��� #��z��#��z��._��� $��Center=�$��Center=��� -   $��238,374,93,59q�
������ 
������ $
 Hand��.���� -   $ 	NextTrack��$��/Images/Next.png��$
 Hand������ " ��.���  � �� �� " ��.R��� #��z��#��z��._��� $��Center=�$��Center=��� -   $��93,374,240,59q�
������ 
������ $
 Hand��.���� -   $ 	PrevTrack��$��/Images/Prev.png������ " ��.���  � �� �� " ��.R��� #��z��#��z��._��� $��Center=�$��Center=��� -    ��Name$ ProgressSlider�� � Minimum$ 0Q� � Maximum$	 100Q� � Value$ 0Q�$��26,334,27,117q� �� TickFrequency$ 1Q��� -   $ VolumeSlider��$��26,433,27,17q�$ 0Q�$	 100Q� �� IsSnapToTickEnabled$
 True��