using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class CelestialBody : GravityObject
{
    public float radius;
    public float orbitRadius;
    public float surfaceGravity;
    public float mass;
    public Vector3 initialVelocity;
    public string bodyName = "Unnamed";
    Transform meshHolder;

    public Vector3 velocity { get; private set; }
    public float massPlanet { get; private set; }
    public float massStar { get; private set; }
    Rigidbody rb;

    public void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        velocity = initialVelocity;
        transform.localPosition = new Vector3 (-orbitRadius, 0, 0);
    }

    public void UpdateVelocity(CelestialBody[] allBodies, float timeStep) {
        foreach (var otherBody in allBodies) {
            if (otherBody != this) {
                float sqrDst = (otherBody.rb.position - rb.position).sqrMagnitude;
                Vector3 forceDir = (otherBody.rb.position - rb.position).normalized;

                Vector3 acceleration = forceDir * Universe.gravitationalConstant * otherBody.mass / sqrDst;
                velocity += acceleration * timeStep;
            }
        }
    }

    public void UpdateVelocity(Vector3 acceleration, float timeStep) {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition(float timeStep) {
        rb.MovePosition(rb.position + velocity * timeStep);

    }

    void OnValidate()
    {
        if (gameObject.name != "Sun")
        {
            massPlanet = (float)(surfaceGravity * radius * radius / Universe.gravitationalConstant);
        }
        else
        {
            massStar = (float)(surfaceGravity * radius * radius * 2.5 / Universe.gravitationalConstant);
        }
        mass = massPlanet + massStar;
        meshHolder = transform.GetChild(0);
        meshHolder.localScale = Vector3.one * radius;
        gameObject.name = bodyName;
    }

    public void CreateBody(float radiusCreate, float orbitRadiusCreate, float surfaceGravityCreate, string name)
    {
        radius = radiusCreate;
        orbitRadius = orbitRadiusCreate;
        surfaceGravity = surfaceGravityCreate;
        bodyName = name;
        OnValidate();
        Awake();
    }

    public Rigidbody Rigidbody {
        get {
            return rb;
        }
    }

    public Vector3 Position {
        get {
            return rb.position;
        }
    }
}
/*
<? php
  require_once "header.php";

  $showmap = 2;

?>
  < body >
    < div id = 'map' style = 'width: 1362px; height: 710px;' ></ div >
   
       < script >



       var map = L.map('map', {
maxZoom: 1,
        minZoom: 1,
        crs: L.CRS.Simple
       }).setView([0, 0], 1);

map.setMaxBounds(new L.LatLngBounds([0, 681], [355, 0]));

function changeMap(island)
{
    image.setUrl('resources/' + island + '.png');
}

var imageBounds = [[355, 0], [0, 681]];
var imageUrl = 'resources/map.png';
var image = L.imageOverlay(imageUrl, imageBounds);
map.addLayer(image);

var SAT = L.polygon([

    [109, 330],

    [109, 243],

    [25, 243],

    [25, 330]
    ], {color: '#0', opacity: 0, fillOpacity: 0}).addTo(map);

var SWT = L.polygon([

    [85, 373],

    [85, 437],

    [19, 437],

    [19, 373]
    ], {color: '#0', opacity: 0, fillOpacity: 0}).addTo(map);


var Kyoshi = L.polygon([

    [153, 418],

    [153, 464],

    [98, 464],

    [98, 418]
    ], {color: '#0', opacity: 0, fillOpacity: 0}).addTo(map);

var Swamp = L.polygon([

    [170, 433],

    [175, 466],

    [210, 452],

    [202, 422]
    ], {color: '#0', opacity: 0, fillOpacity: 0}).addTo(map);

var Desert = L.polygon([

    [230, 445],

    [175, 466],

    [163, 502],

    [169, 534],

    [187, 549],

    [207, 558],

    [255, 555],

    [241, 520],

    [231, 498]
    ], {color: '#0', opacity: 0, fillOpacity: 0}).addTo(map);

var BSS = L.polygon([

  [233, 464],

  [236, 499],

  [254, 522],

  [283, 522],

  [304, 503],

  [302, 467],

  [282, 441],

  [260, 449]
    ], {color: '#0', opacity: 0, fillOpacity: 0}).addTo(map);

var NWT = L.polygon([

    [320, 322],

    [274, 333],

    [280, 361],

    [325, 344]
    ], {color: '#0', opacity: 0, fillOpacity: 0}).addTo(map);

var WAT = L.polygon([

    [273, 304],

    [310, 261],

    [300, 238],

    [283, 225],

    [265, 245],

    [248, 278]
    ], {color: '#0', opacity: 0, fillOpacity: 0}).addTo(map);

var Roku = L.polygon([

    [215, 358],

    [237, 318],

    [247, 334],

    [246, 352],

    [233, 360]
    ], {color: '#0', opacity: 0, fillOpacity: 0}).addTo(map);

var FN = L.polygon([

    [146, 290],

    [184, 305],

    [199, 305],

    [233, 290],

    [238, 190],

    [244, 170],

    [250, 142],

    [236, 101],

    [205, 77],

    [171, 78],

    [152, 84],

    [147, 106],

    [142, 165],
    ], {color: '#0', opacity: 0, fillOpacity: 0}).addTo(map);

var satpopup = L.DomUtil.create('div', 'popup');
satpopup.innerHTML = '<div class = "satpopup"><h1>Southern Air Temple</h1> <br> Click here to see the map of the Southern Air Temple.</div>';
var swtpopup = L.DomUtil.create('div', 'popup');
swtpopup.innerHTML = '<div class = "swtpopup"><h1>Southern Water Tribe</h1> <br> Click here to see the map of the Southern Water Tribe..</div>';
var kyoshipopup = L.DomUtil.create('div', 'popup');
kyoshipopup.innerHTML = '<div class = "kyoshipopup"><h1><h1>Kyoshi Island</h1> <br> Click here to see the map of Kyoshi Island.</div>';
var desertpopup = L.DomUtil.create('div', 'popup');
desertpopup.innerHTML = '<div class = "desertpopup"><h1>Desert</h1> <br> Click here to see the map of the Desert.</div>';
var swamppopup = L.DomUtil.create('div', 'popup');
swamppopup.innerHTML = '<div class = "swamppopup"><h1>Swamp</h1> <br> Click here to see the map of the Swamp.</div>';
var bsspopup = L.DomUtil.create('div', 'popup');
bsspopup.innerHTML = '<div class = "bsspopup"><<h1>Ba Sing Se</h1> <br> Click here to see the map of Ba Sing Se.</div>';
var nwtpopup = L.DomUtil.create('div', 'popup');
nwtpopup.innerHTML = '<div class = "nwtpopup"><h1>Northern Water Tribe</h1> <br> Click here to see the map of the Northern Water Tribe.</div>';
var rokupopup = L.DomUtil.create('div', 'popup');
rokupopup.innerHTML = '<div class = "rokupopup"><h1>Roku\'s Temple</h1> <br> Click here to see the map of Roke\'s Temple.</div>';
var watpopup = L.DomUtil.create('div', 'popup');
watpopup.innerHTML = '<div class = "watpopup"><h1>Western Air Temple</h1> <br> Click here to see the map of the Western Air Temple.</div>';
var fnpopup = L.DomUtil.create('div', 'popup');
fnpopup.innerHTML = '<div class = "fnpopup"><h1>Fire Nation</h1> <br> Click here to see the map of the Fire Nation.</div>';

    $('div.satpopup', satpopup).on('click', function() {
    changeMap(othermap);
});
    $('div.swtpopup', swtpopup).on('click', function() {
    alert("click");
});
    $('div.kyoshipopup', kyoshipopup).on('click', function() {
    alert("click");
});
    $('div.desertpopup', desertpopup).on('click', function() {
    alert("click");
});
    $('div.swamppopup', swamppopup).on('click', function() {
    alert("click");
});
    $('div.bsspopup', bsspopup).on('click', function() {
    alert("click");
});
    $('div.nwtpopup', nwtpopup).on('click', function() {
    alert("click");
});
    $('div.rokupopup', rokupopup).on('click', function() {
    alert("click");
});
    $('div.watpopup', watpopup).on('click', function() {
    alert("click");
});
    $('div.fnpopup', fnpopup).on('click', function() {
    alert("click");
});

SAT.bindPopup(satpopup);
SWT.bindPopup(swtpopup);
Kyoshi.bindPopup(kyoshipopup);
Swamp.bindPopup(swamppopup);
Desert.bindPopup(desertpopup);
BSS.bindPopup(bsspopup);
NWT.bindPopup(nwtpopup);
Roku.bindPopup(rokupopup);
WAT.bindPopup(watpopup);
FN.bindPopup(fnpopup);


    </ script >

    </ body >
  </ html >
*/