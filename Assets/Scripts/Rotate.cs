using UnityEngine;

public class Rotate : MonoBehaviour
{
	public Vector3 direction = Vector3.zero;
	public float speed = 5f;

    private Transform _transform = null;

	private void Awake()
	{
		_transform = GetComponent<Transform>();
	}

	private void FixedUpdate()
    {
		_transform.Rotate(direction * speed * Time.deltaTime);
    }
}
