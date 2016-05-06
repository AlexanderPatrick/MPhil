/*\
 *	How to give the NPC "Eyes".
 *	Approach #1 - Give each NPC a camera component.
 *	  	The camera takes a screenshot of what it renders and puts that into a Texture2D.
 *	  	That Texture2D is analysed to extract features. However, it does not work on the entire image in one update cycle (coroutines and yields).
 *			This is the important part I would need to figure out how to do. (Naive intuitive guess is edge detection algorithms?)
 *		In subsequent cycles a new picture may be retrieved and the analysis carries over from the previous cycle.
 *			It then becomes possible to detect motion over frames, though the NPC needs to be able to compensate for its own movement.
 
 *	Approach #2 - A Trigger Collider representing the view area that adds objects to a list,
 *	then that list of objects has its mesh vertices tested with raycasts to check for occlusion. 
 *	May require some form of object annotation so the objects are responsible for informing the agent of what it is.
\*/