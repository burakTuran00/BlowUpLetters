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
    [SerializeField] private float slicedForce = 5.0f;
    [SerializeField] private float minSliceVelocity = 0.01f;
    [SerializeField] private TrailRenderer bladeTrail;
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
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
        //gameManager.GetRidOfSentence();
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0.0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
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

            /*Letter letter = hit.transform.GetComponent<Letter>();

            if (letter != null)
            {
                gameManager.AddSentence(letter.getLetterChar());
            }*/

            // Perform slicing action here if needed
            //Letter letter = hit.transform.GetComponent<Letter>();
            //gameManager.AddSentence(letter.getLetterChar());
        }
    }

    public void BlastEffect()
    {
        blastEffect.Play();
        blastAudioEffect.Play();
    }
}
