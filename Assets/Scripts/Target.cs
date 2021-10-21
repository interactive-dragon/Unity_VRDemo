using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public TargetProperties m_Properties { get; protected set; }
    private Vector3 m_EulerAngleVel = new Vector3(0,100,0);
    private Rigidbody m_RBody;
    private TMPro.TextMeshPro[] m_Labels;
    private Material m_Material;
    [SerializeField]
    private GameObject m_Explosion;
    [SerializeField]
    private bool m_isDemo = false;

    private bool m_GameOver = false;
    // Start is called before the first frame update
    void Awake()
    {
        m_RBody = GetComponent<Rigidbody>();
        m_Labels = GetComponentsInChildren<TMPro.TextMeshPro>();
        m_Material = GetComponent<MeshRenderer>().material;
        GameController.Instance.OnGameEnd += OnGameEnd;
    }

    private void OnDestroy()
    {
        GameController.Instance.OnGameEnd -= OnGameEnd;
    }
    private void OnGameEnd()
    {
        m_GameOver = true;
    }
    public void Init(TargetProperties pProperties)
    {
        m_Properties = pProperties;
        foreach (TMPro.TextMeshPro label in m_Labels)
            label.text = m_Properties.Points.ToString();

        m_Material.SetColor("_MainColor", m_Properties.Color);
        m_Material.SetColor("_GlowColor", m_Properties.Color);
    }
    private void FixedUpdate()
    {
        if (m_GameOver)
            return;

        if (!m_isDemo)
        {
            Vector3 newPos = transform.position + new Vector3(0, m_Properties.MovementSpeed * Time.deltaTime, 0);
            m_RBody.MovePosition(newPos);
        }
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVel * Time.fixedDeltaTime);
        m_RBody.MoveRotation(m_RBody.rotation * deltaRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ShotBehavior>() != null)
        {
            GameController.Instance.AddScore(m_Properties.Points);
            GameObject ex =  GameObject.Instantiate(m_Explosion, other.transform.position, Quaternion.identity);
            GameObject.Destroy(other.gameObject);
            GameObject.Destroy(this.gameObject);
            GameObject.Destroy(ex, 3.0f);
        }
    }
}

public class TargetProperties
{
    public float MovementSpeed { get; protected set; }
    public int Points { get; protected set; }
    public Color Color { get; protected set; }

    public TargetProperties(float pMovementSpeed, int pPoints, Color pColor)
    {
        MovementSpeed = pMovementSpeed;
        Points = pPoints;
        Color = pColor;
    }
    
}
