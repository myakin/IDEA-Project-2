using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesData {
    public List<int> collectedKeyIds;

    public CollectiblesData() {}

    public CollectiblesData(List<int> keyIds) {
        this.collectedKeyIds = keyIds;
    }

}
