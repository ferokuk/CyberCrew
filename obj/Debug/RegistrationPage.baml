   M S B A M L   `   `   `  ����D  @CyberCrew, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CyberCrew	CyberCrew  5           CyberCrew.RegistrationPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:CyberCrew     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5        N� Title$  RegistrationPage��5
      #��Z��5      #��Z��5      .��5      � ��5   
   �� 5       �� 
StartPoint$	 0,1&�6"    �� EndPoint$	 0,0&�5   "   .��5      �  � Color$ #452701��6    � Offset$ 0Q�5      � 5      $ #26221f��6   $ 1Q�5      5       � RowDefinitions 5   
   �� 5      $	��23* �6   �� 5      $��9* �6   �� 5      $��1* �6   �� 5      $��9* �6   �� 5      $��1* �6   �� 5      $��9* �6   �� 5       $��1* �6   �� 5!      $��9* �6   �� 5"      $��1* �6   �� 5#      $
��8.5* �6   �� 5$      $
��8.5* �6   �� 5%      $	��20* �6    � ColumnDefinitions 5'   
   �� 5(      $	��15* �6   �� 5)      $	��15* �6   �� 5*      $	��10* �6   �� 5+      $	��20* �6   �� 5,      $	��10* �6   �� 5-      $	��15* �6   �� 5.      $	��15* �6   .+�50   
   �� $��resources/big_logo.png��6   $��2��51      $��0��52      $��3��53      �� 54   
   $��Center=�6   $��1��55      $��2��56      $
��Wrap=�57      $��Center=�58      ���	#FFFF8A00$��50(�5:      $��Days One)�5;      $��
Логин��5<      �� 5=   
   $��Center=�6   $��3��5>      $��2��5?      $
��Wrap=�5@      $��Center=�5A      ���	#FFFF8A00$��50(�5C      $��Days One)�5D      $��
Почта��5E      �� 5F   
   $��Center=�6   $��5��5G      $��2��5H      $
��Wrap=�5I      $��Center=�5J      ���	#FFFF8A00$��50(�5L      $��Days One)�5M      $��Пароль��5N      �� 5O   
   $��Center=�6   $��7��5P      $��2��5Q      $
��Wrap=�5R      $��Center=�5S      ���	#FFFF8A00$��50(�5U      $��Days One)�5V      $%��Пароль (повторно)��5W      �� 5X   
   -   5         ��Name$ Login��5\         TextBox#���   5X      $��1��5Y      $��2��5Z      $��3��5[       �� 	MaxLength$ 20��5]   2   �� 5^   
   -   5        $ Email��5b      #���   5^      $��3��5_      $��2��5`      $��3��5a      $	 100��5c   2   G� 5d   
   -   5        	 G�Name$	 Password��5h        PasswordBox#���  5d      $��5��5e      $��2��5f      $��3��5g      5i      G� 5j   
   -   5        $	 RepeatPassword��5n      #���  5j      $��7��5k      $��2��5l      $��3��5m      5o      �� 5p   
   -   5        
 ��Name$
 Register��5v        ConfirmButton#���  5p      $��10��5q      $��2��5r      $��3��5s      $#��Создать аккаунт��5t      $��45(�5u      {�.  5x      �� 5y   
   -   5          
MenuButton#���  5y      $)��#У меня есть аккаунт��5z      $��2��5{      $��11��5|      $��3��5}      $	��Top=�5~      $��Center=�5      $��40(�5�      $��Montserrat ExtraLight)�5�      $��0q�5�      $��Center=�5�      $��60��5�      $	��514��5�      5�      �� 5�   
   -   5         ��Name$ LoginMessage��5�      $��Days One)�5�      $��5��5�      $��2��5�      $
��Left=�5�      $��1��5�      $
��Wrap=�5�      $�� ��5�      $��Center=�5�      $��25(�5�      �� 5�   
   -   5        $ PasswordMessage��5�      $��Days One)�5�      $��5��5�      $��2��5�      $
��Left=�5�      $��7��5�      $
��Wrap=�5�      $�� ��5�      $��Center=�5�      $��25(�5�      �� 5�   
   -	   5        $ EmailMessage��5�      $��Days One)�5�      $��5��5�      $��2��5�      $
��Left=�5�      $��3��5�      $
��Wrap=�5�      $�� ��5�      $��Center=�5�      $��25(�5�      �� 5�   
   -
   5        $ RegisterMessage��5�      $��Days One)�5�      $��2��5�      $��3��5�      $
��Left=�5�      $��9��5�      $
��Wrap=�5�      $�� ��5�      $��Center=�5�      $��30(�5�      5�      5�      