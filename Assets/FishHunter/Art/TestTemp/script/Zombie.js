#pragma strict

public var victim : Transform;
private var navComponent : NavMeshAgent;
function Start () {

  navComponent = this.transform.GetComponent(NavMeshAgent);

}

function Update () {

   if(victim){
        navComponent.SetDestination(victim.position);
       //navComponent.setDestination(tagetpoint.position);
       //https://www.youtube.com/watch?v=DcJSz5c4VGk&list=UUGre1-OlqgofGzl93TEP4tw&src_vid=pXHVFRHbgY0&feature=iv&annotation_id=annotation_618016775
   }

}