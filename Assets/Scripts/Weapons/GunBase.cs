using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GunBase : MonoBehaviour
{

    public bool automaticFire = false;
    public bool infiniteCharges = false;
    public int damage = 20;
    public float maxRange = 50f;
    public float fireDelay = 0.25f;

    public int ammo;
    public int maxAmmo = 0;
    public int charges;
    public int maxCharges;
    public string primaryFire = "Fire1";
    public float startRecoil = 3f;
    public float recoilDampening = 0.1f;
    public float recoilStop = -0.7f;
    public Transform gunEnd;

    protected Animator anim;
    protected WeaponManager weaponManager;
    protected Vector3 aim;
    protected Camera cam;
    protected AudioSource gunAudio;
    protected bool readyToFire = true;
    private WaitForSeconds recoilWait = new WaitForSeconds(0.05f);
    protected Text ammoText;
    protected Text chargesText;

    protected abstract void PrimaryFire();
	
    void Start()
    {
        weaponManager = GetComponentInParent<WeaponManager>();
        cam = GetComponentInParent<Camera>();
        gunAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
        chargesText = GameObject.Find("ChargesText").GetComponent<Text>();
    }
	void FixedUpdate ()
    {
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, maxRange))
        {
            aim = hit.point;
        }
        else
        {
            aim = rayOrigin + (cam.transform.forward * maxRange);
        }
        
        bool primaryFirePressed;
        if (automaticFire)
        {
            primaryFirePressed = Input.GetButton(primaryFire);
        }
        else
        {
            primaryFirePressed = Input.GetButtonDown(primaryFire);
        }
        
        if (primaryFirePressed)
        {
            if (readyToFire && ammo > 0)
            {
                PrimaryFire();
                ammo--;
                StartCoroutine(Recoil(startRecoil));
                readyToFire = false;
                Invoke("SetReadyToFire", fireDelay);
            }
        }

        if (charges > 0 || infiniteCharges)
        {
            if (Input.GetKeyDown(KeyCode.R) && readyToFire && maxAmmo != 0 && maxAmmo != ammo)
            {
                Reload();
            }
        }

        ammoText.text = "" + ammo;
        if (infiniteCharges)
            chargesText.text = "";
        else
            chargesText.text = "" + charges;
    }

    private IEnumerator Recoil(float recoil)
    {
        cam.transform.Rotate(new Vector3(-recoil, 0f, 0f));
        yield return recoilWait;
        recoil -= recoilDampening;
        if (recoil > recoilStop)
            StartCoroutine(Recoil(recoil));
    }

    void SetReadyToFire()
    {
        readyToFire = true;
    }

    void Reload()
    {
        readyToFire = false;
        anim.SetTrigger("Reload");
        charges--;
        ammo = maxAmmo;
        Invoke("SetReadyToFire", 1.5f);
    }

    public void AddCharges(int newcharges)
    {
        charges += newcharges;
        if (charges > maxCharges)
            charges = maxCharges;
    }
}
