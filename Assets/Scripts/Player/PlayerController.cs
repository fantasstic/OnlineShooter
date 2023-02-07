using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f, _rotateSpeed = 4f, _force = 2f;
    [SerializeField] private int _damage = 25;
    [SerializeField] private int _health = 100;
    [SerializeField] private ParticleSystem _shootEffect;

    private Rigidbody _rigidbody;
    private PhotonView _photonView;

    public bool IsGround = false;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidbody = GetComponent<Rigidbody>();

        if (!_photonView.IsMine)
            Destroy(GetComponentInChildren<Camera>().gameObject);
    }

    private void FixedUpdate()
    {
        if (!_photonView.IsMine)
            return;

        MovePlayer();
    }

    private void Update()
    {
        if (!_photonView.IsMine)
            return;

        RotatePlayer();

        Shoot();
    }

    private void MovePlayer()
    {
        _rigidbody.MovePosition(transform.position + (transform.forward * Time.fixedDeltaTime * _speed * Input.GetAxis("Vertical")));

    }

    private void RotatePlayer()
    {
        transform.Rotate(Vector3.up * _rotateSpeed * Input.GetAxis("Horizontal"));
    }

    private void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 shootPoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Camera camera = transform.GetChild(0)?.GetComponent<Camera>();
            Ray ray = camera.ScreenPointToRay(shootPoint);
            

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Quaternion hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                Instantiate(_shootEffect, hit.point, hitRotation);
                if(hit.collider.CompareTag("Player"))
                {
                    hit.collider.GetComponent<PlayerController>().Damage(_damage);
                    hit.collider.GetComponent<PlayerController>().AddForcePlayer(_force);
                }
            }
        }
    }

    public void Damage(int damage)
    {
        _photonView.RPC("PunDamage", RpcTarget.All, damage);
    }

    public void AddForcePlayer(float force)
    {
        _photonView.RPC("PunAddForce", RpcTarget.All, force);
    }

    [PunRPC]
    private void PunDamage(int damage)
    {
        if (!_photonView.IsMine)
            return;

        _health -= damage;
        if (_health <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    private void PunAddForce(float force)
    {
        if (!_photonView.IsMine)
            return;

            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
}
