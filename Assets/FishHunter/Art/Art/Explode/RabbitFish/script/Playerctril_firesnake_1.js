var GameObj :GameObject;

var move :int;

var speed :float;

function Start () {

move= 4 ;
}

function Update () {
  

 if(Input.GetKey(KeyCode.A))
{
   move = 2 ;
}
if(Input.GetKey(KeyCode.D))
{
   move = 4 ;
}
if(Input.GetKey(KeyCode.W))
{
   move = 6 ;
}
if(Input.GetKey(KeyCode.S))
{
   move = 8 ;
}
   if( move == 2 )
   {
       this.transform.position.x = this.transform.position.x + (-1*speed*Time.deltaTime);
   }
   if( move == 4 )
   {
       this.transform.position.x = this.transform.position.x + (speed*Time.deltaTime);
   }
 if( move == 6 )
   {
       this.transform.position.y = this.transform.position.y + (speed*Time.deltaTime);
   }
    if( move == 8 )
   {
       this.transform.position.y = this.transform.position.y + (-1*speed*Time.deltaTime);
   }
   if(Input.GetKey(KeyCode.W)){
  gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0.0f, 0.0f, -270.0f), Time.deltaTime * 100f);
     }
     
       if(Input.GetKey(KeyCode.S)){
  gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0.0f, 0.0f, 270.0f), Time.deltaTime * 100f);
     }

 if(Input.GetKey(KeyCode.D)){

	gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 100f);
	 }
     if(Input.GetKey(KeyCode.A)){
  gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0.0f, 0.0f, 180.0f), Time.deltaTime * 100f);
     }
  }
    