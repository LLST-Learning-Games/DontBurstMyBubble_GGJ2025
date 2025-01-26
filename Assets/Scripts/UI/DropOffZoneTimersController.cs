using System.Linq;
using UnityEngine;

public class DropOffZoneTimersController : MonoBehaviour
{
    [SerializeField] private DropOffZoneTimer _prefab;

    private void Start()
    {
        var annihilators = FindObjectsByType<Annihilator>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .OrderBy(x => x.Index);

        foreach (Annihilator annihilator in annihilators)
        {
            DropOffZoneTimer entry = Instantiate(_prefab, transform, false);
            entry.Annihilator = annihilator;
            entry.gameObject.SetActive(true);
        }
    }
}
