var ammo :GameObject;

var GunFire :Transform;

var CD :float;
var Timer :float;
var Firespeed :float;

var speed :float;

function Start () {

}

function Update () {
  
   //if(Input.GetKey(KeyCode.Space)){
   /*
     if(Input.GetKey(KeyCode.Mouse0)){
      Fire();
   
   }
   */
  

//   if(Input.GetKey(KeyCode.W)){
//   this.transform.position.x = this.transform.position.x + (speed*Time.deltaTime);
// }  
// if(Input.GetKey(KeyCode.S)){
//   this.transform.position.x = this.transform.position.x + (-1*speed*Time.deltaTime);
//}
/*/
 if(Input.GetKey(KeyCode.E)){
   this.transform.rotation.y = this.transform.rotation.y + (speed*Time.deltaTime);
   }
 if(Input.GetKey(KeyCode.Q)){
   this.transform.rotation.y = this.transform.rotation.y + (-1*speed*Time.deltaTime);
    }
    */
//}

/*
function Fire () {
   CD = 1/(Firespeed/100);
   if(Timer>CD){
      Timer = 0;
      //Instantiate(AAAAA,this.transform.position,this.transform.rotation);
      Instantiate(ammo,GunFire.transform.position,GunFire.transform.rotation);
      //Instantiate(ammo,this.transform.position,this.transform.rotation);
    }else{
    Timer = Timer + Time.deltaTime;
    
 }
 */
}