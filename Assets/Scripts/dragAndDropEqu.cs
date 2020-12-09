using UnityEngine;
using UnityEngine.EventSystems;

public class dragAndDropEqu : MonoBehaviour,IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler {
    Canvas canvas;
    RectTransform rectTransform;
    public Vector3 rtBegin;
    CanvasGroup canvasGroup;
    void Start() {
        canvas = StaticMethods.FindInActiveObjectByName("CanvasUI").GetComponent<Canvas>();
    }
    private void Awake() {
        rectTransform = this.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("Begin Drag");
        rtBegin = rectTransform.anchoredPosition;
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        StaticMethods.FindInActiveObjectByName("InventoryCatcher").SetActive(true);

    }

    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("End Drag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        rectTransform.anchoredPosition = rtBegin;
        StaticMethods.FindInActiveObjectByName("InventoryCatcher").SetActive(false);

    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("Clicked");
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop ");

    }
}
