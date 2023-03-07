using UnityEngine;

[CreateAssetMenu]
public class InstrumentDrop : DropData
{
    public Instrument Instrument;

    public override GameObject Create(GameObject at)
    {
        var inst = Instantiate(Instrument.Prefab, at.transform.position + at.transform.forward * 3 + Vector3.up * 2,
            Quaternion.identity);
        inst.AddComponent<Rigidbody>();
        inst.AddComponent<DroppedInstrument>().Instrument = Instrument;
        return inst;
    }
}