   M S B A M L   `   `   `  ���� K  GDnDCombatTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DnDCombatTrackerDnDCombatTracker  %    DnDCombatTracker.AddEnemyWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:DnDCombatTracker       � Title$  AddEnemyWindow��$	��450��$	��800�����	#FF9B9B9B.��� .+��� -    ��Name$ EnemyNameTextBox��$��Center=�$��0,10,0,0q� �� TextWrapping$
 Wrap=�$��
Enemy Name��$	��Top=�$	��250��$��24(�$��50���� -   $��
StrTextbox��$
��Left=�$��
55,150,0,0q�$
 Wrap=�$��0��$	��Top=�$��50��$��50�����	#FFCEC5C5�� -   $��
DexTextbox��$
��Left=�$��154,150,0,0q�$
 Wrap=�$��0��$	��Top=�$��50��$��50�����	#FFCEC5C5�� -   $��
ConTextbox��$
��Left=�$��275,150,0,0q�$
 Wrap=�$��0��$	��Top=�$��50��$��50�����	#FFCEC5C5�� -   $��
IntTextbox��$
��Left=�$��475,150,0,0q�$
 Wrap=�$��0��$	��Top=�$��50��$��50�����	#FFCEC5C5�� -   $��
WisTextbox��$
��Left=�$��601,150,0,0q�$
 Wrap=�$��0��$	��Top=�$��50��$��50�����	#FFCEC5C5�� -   $��
ChaTextbox��$
��Left=�$��712,150,0,0q�$
 Wrap=�$��0��$	��Top=�$��50��$��50�����	#FFCEC5C5�� $	��STR��$
��Left=�$��
42,119,0,0q�$	��Top=�$��50���� $	��DEX��$
��Left=�$��129,119,0,0q�$	��Top=�$��50���� $	��CON��$
��Left=�$��250,119,0,0q�$	��Top=�$��50���� $	��INT��$
��Left=�$��450,124,0,0q�$	��Top=�$��50���� $	��WIS��$
��Left=�$��576,124,0,0q�$	��Top=�$��50���� $	��CHA��$
��Left=�$��687,124,0,0q�$	��Top=�$��50���� $��HP��$
��Left=�$��
576,17,0,0q�$	��Top=��� -   $��	HpTextBox��$
��Left=�$��
576,43,0,0q�$
 Wrap=�$��0��$	��Top=�$��30��$��25�� J� RenderTransformOrigin$ 2.162,0.517&��� $��AC��$
��Left=�$��
626,17,0,0q�$	��Top=��� -	   $��	AcTextBox��$
��Left=�$��
626,43,0,0q�$
 Wrap=�$��0��$	��Top=�$��30��$��25��$ 2.162,0.517&��� -
   $��noteTextBox��$��Center=�$��	0,275,0,0q�$
 Wrap=�$��No notes��$	��Top=�$	��620��$	��102���� $��Additional Notes��$
��Left=�$��
90,244,0,0q�$	��Top=��� -   $��CancelButton��$��Cancel��$
��Left=�$��
105,17,0,0q�$	��Top=�$��30��$��70���� -   $��
SaveButton��$
��Save��$
��Left=�$��	10,17,0,0q�$	��Top=�$��30��$��70���� -   $��closeButton��$��Close��$
��Left=�$��
712,11,0,0q�$	��Top=�$��39��$��65���� $��	Hit dice?��$��Center=�$��0,60,0,0q�$	��Top=��� -   $��hitDiceBoxAmount��$
��Left=�$��
364,86,0,0q�$
 Wrap=�$��0��$	��Top=�$��30��$��25���� -   $��hitDiceBoxSize��$
��Left=�$��
412,86,0,0q�$
 Wrap=�$��0��$	��Top=�$��30��$��25���� $��D��$
��Left=�$��
394,85,0,0q�$	��Top=��� -   $ hitDiceBoxModifier��$
��Left=�$��
495,86,0,0q�$
 Wrap=�$��0��$	��Top=�$��30��$��25���� -   $��hitDiceComboBox��$
��Left=�$��
446,88,0,0q�$	��Top=�$��44��.@��� $��+���� $��-���� $��+/- ?��$
��Left=�$��
454,62,0,0q�$	��Top=�