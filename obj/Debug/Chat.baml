   M S B A M L   `   `   `  ����B  >CHESSim, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:CHESSimCHESSim  5   	       CHESSim.Chat   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:CHESSim     5   	     � Title$
  Chat��5   	   $	��500��6   $	��300��6#   .��5	      �  � ColumnDefinitions 5
      �� 5      �� 5      �� 5       � RowDefinitions 5      �� 5      $
��156* �6   �� 5      $	��156 �6   �� 5      $
��157* �6   .+�5      �� 
�������$��3��6!   $��3��62   ��6F   �� *ChatNachrichtenLB+6r   �� 5      $��3��6   $��2��6    $��Bottom=�6-   
�������$��67,0,0,4q�6{   $��14��6�   ��6\   �� *ChatNachricht+.��6�    J� InputBindings 5      �� 5      
 �� Key$ Enter��6    �� Command 6   �� *SendMessage+.��5      5      �� 5      $��0��6
   $��3��6   $��Bottom=�6'   $
��Left=�6B   $��Nachricht: ��6]   6u   5      5      