var target : Transform;
var distance = 10.0;
var xSpeed = 250.0;
var ySpeed = 120.0;
var yMinLimit = -20;
var yMaxLimit = 80;
var zoomRate = 25;
var yOffset:float;

private var x = 0.0;
private var y = 0.0;

@script AddComponentMenu("Camera-Control/Mouse Orbit")

function Start () {
	var t:Transform = GameObject.Find("Camera Target").transform;
	if(t)
	target = t;
    var angles = transform.eulerAngles;
    x = angles.y;
    y = angles.x;
}
	
function Update () {
	if (!Input.GetMouseButton(0)){
			 	

    if (target) {
	
    
		x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
      
        distance += -(Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
      
       		
 		y = ClampAngle(y, yMinLimit, yMaxLimit);
 		       
        var rotation = Quaternion.Euler(y, x, 0);
        var t:Vector3 = Vector3(target.position.x, target.position.y + yOffset, target.position.z);
        var position = rotation * Vector3(0.0, 0.0, -distance) + t;
        
        transform.rotation = rotation;
        transform.position = position;
         
    }
	
}
}

static function ClampAngle (angle : float, min : float, max : float) {
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}