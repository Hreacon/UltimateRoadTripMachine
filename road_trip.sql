��U S E   [ r o a d _ t r i p ]  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ d e s t i n a t i o n s ]         S c r i p t   D a t e :   3 / 8 / 2 0 1 6   9 : 2 7 : 5 4   A M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 S E T   A N S I _ P A D D I N G   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ d e s t i n a t i o n s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ n a m e ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ r o a d t r i p _ i d ]   [ i n t ]   N U L L ,  
 	 [ s t o p ]   [ i n t ]   N U L L  
 )   O N   [ P R I M A R Y ]   
  
 G O  
 S E T   A N S I _ P A D D I N G   O F F  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ i m a g e s ]         S c r i p t   D a t e :   3 / 8 / 2 0 1 6   9 : 2 7 : 5 4   A M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 S E T   A N S I _ P A D D I N G   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ i m a g e s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ l i n k ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ s e a r c h _ t e r m s _ i d ]   [ i n t ]   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 S E T   A N S I _ P A D D I N G   O F F  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ r o a d t r i p s ]         S c r i p t   D a t e :   3 / 8 / 2 0 1 6   9 : 2 7 : 5 4   A M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 S E T   A N S I _ P A D D I N G   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ r o a d t r i p s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ n a m e ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ d e s c r i p t i o n ]   [ v a r c h a r ] ( 5 0 0 0 )   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 S E T   A N S I _ P A D D I N G   O F F  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ s e a r c h _ t e r m s ]         S c r i p t   D a t e :   3 / 8 / 2 0 1 6   9 : 2 7 : 5 4   A M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 S E T   A N S I _ P A D D I N G   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ s e a r c h _ t e r m s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ t e r m ]   [ v a r c h a r ] ( 2 5 5 )   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 S E T   A N S I _ P A D D I N G   O F F  
 G O  
 S E T   I D E N T I T Y _ I N S E R T   [ d b o ] . [ d e s t i n a t i o n s ]   O N    
  
 I N S E R T   [ d b o ] . [ d e s t i n a t i o n s ]   ( [ i d ] ,   [ n a m e ] ,   [ r o a d t r i p _ i d ] ,   [ s t o p ] )   V A L U E S   ( 1 ,   N ' m u l t n o m a h   f a l l s ' ,   1 ,   1 )  
 I N S E R T   [ d b o ] . [ d e s t i n a t i o n s ]   ( [ i d ] ,   [ n a m e ] ,   [ r o a d t r i p _ i d ] ,   [ s t o p ] )   V A L U E S   ( 2 ,   N ' p a r a d i s e   f a l l s ' ,   1 ,   2 )  
 I N S E R T   [ d b o ] . [ d e s t i n a t i o n s ]   ( [ i d ] ,   [ n a m e ] ,   [ r o a d t r i p _ i d ] ,   [ s t o p ] )   V A L U E S   ( 3 ,   N ' b u r g e r v i l l e ' ,   1 ,   3 )  
 I N S E R T   [ d b o ] . [ d e s t i n a t i o n s ]   ( [ i d ] ,   [ n a m e ] ,   [ r o a d t r i p _ i d ] ,   [ s t o p ] )   V A L U E S   ( 4 ,   N ' a q u a r i u m ' ,   1 ,   4 )  
 S E T   I D E N T I T Y _ I N S E R T   [ d b o ] . [ d e s t i n a t i o n s ]   O F F  
 S E T   I D E N T I T Y _ I N S E R T   [ d b o ] . [ r o a d t r i p s ]   O N    
  
 I N S E R T   [ d b o ] . [ r o a d t r i p s ]   ( [ i d ] ,   [ n a m e ] ,   [ d e s c r i p t i o n ] )   V A L U E S   ( 1 ,   N ' e x c e l l e n t   a d v e n t u r e ' ,   N ' e x c e l l e n t   a d v e n t u r e   t o   f a r a w a y   p l a c e s ' )  
 S E T   I D E N T I T Y _ I N S E R T   [ d b o ] . [ r o a d t r i p s ]   O F F  
 
