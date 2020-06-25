using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//  Keep track of base attributes
public class CharacterProfile : MonoBehaviour {

    [SerializeField]
    private int _baseHealth;
    [SerializeField]
    private float _baseMovementWalk;
    [SerializeField]
    private float _baseMovementRun;
    [SerializeField]
    private float _baseMovementCrouch;
    [SerializeField]
    private Sprite _characterIcon;
    [SerializeField]
    private string _characterName;
    [SerializeField]
    [Range(0,6)]
    private int _baseIntelligence;

    [SerializeField]
    private int _actualHealth;
    [SerializeField]
    private float _actualMovementWalk;
    [SerializeField]
    private float _actualMovementRun;
    [SerializeField]
    private float _actualMovementCrouch;
    [SerializeField]
    [Range(0,6)]
    private int _actualIntelligence;

    public int baseHealth { get { return _baseHealth; } }
    public float baseMovementWalk { get { return _baseMovementWalk; } }
    public float baseMovementRun { get { return _baseMovementRun; } }
    public float baseMovementCrouch { get { return _baseMovementCrouch; } }
    public int baseIntelligence { get { return _baseIntelligence; } }

    public Sprite characterIcon { get { return _characterIcon; } }
    public string characterName { get { return _characterName; } }
    
    public int health { get { return _actualHealth; } set { _actualHealth = value; } }
    public float movementWalk { get { return _actualMovementWalk; }  set { _actualMovementWalk = value; } }
    public float movementRun { get { return _actualMovementRun; }  set { _actualMovementRun = value; } }
    public float movementCrouch { get { return _actualMovementCrouch; }  set { _actualMovementCrouch = value; } }
    public int intelligence { get { return _actualIntelligence; }  set { _actualIntelligence = value; } }


    void Start () {
        ResetAttributes();
    }

    // Reset actual values to defaults (base values)
    public void ResetAttributes() {
        health = baseHealth;
        movementWalk = baseMovementWalk;
        movementRun = baseMovementRun;
        movementCrouch = baseMovementCrouch;
        intelligence = baseIntelligence;
    }
}
