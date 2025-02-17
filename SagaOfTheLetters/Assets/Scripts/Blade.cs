using UnityEngine;

public class Blade : MonoBehaviour
{
    #region Variables
    [SerializeField] private ParticleSystem blastEffect;
    [SerializeField] private AudioSource blastAudioEffect;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Collider bladeCollider;
    [SerializeField] private LayerMask sliceLayerMask;
    public bool slicing {get; private set;}
    public Vector3 direction { get; private set; }
    private bool firstTouch = true;
    #endregion 
   
    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(firstTouch)
            {
                GameManager.Instance.StartGame();
                GameManager.Instance.SetConditionOfStartGameText(false);
                firstTouch = false;
            }

            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicingMain();
        }
    }

    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0.0f;

        transform.position = newPosition;

        slicing = true;

        bladeCollider.enabled = true;
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
    }

    private void ContinueSlicingMain()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, sliceLayerMask))
        {
            Vector3 newPosition = hit.point;
            newPosition.z = 0.0f;

            direction = newPosition - transform.position;
            transform.position = newPosition;
        }
    }
    
    public void BlastEffect()
    {
        blastEffect.Play();
        blastAudioEffect.Play();
    }
}