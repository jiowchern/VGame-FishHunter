//#pragma strict
//var life :GameObject;
var LIFE :float;
function Start () {

}

function Update ()
 {
   //Instantite(ammo,Transform.position.transform.rotation);
// Destroy(this.gameObject,3.0f); //This destroys the object immediately 
   Destroy(this.gameObject,LIFE); //"1"代表1秒之後清掉該物體 
}




