using UnityEngine;

public class TestComponent : MonoBehaviour {

	// We don't know that this method is called somewhere, because 'Find References' don't affect in-direct references
	// like setup UnityEvent callbacks via Inspector
	public void OnClick() {
		// Do some work
	}
}
