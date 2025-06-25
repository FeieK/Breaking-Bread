using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.DedicatedServer;
public class HeartUiElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Image uiHpFill_border;
    [SerializeField] private Image uiHpFill_100;
    [SerializeField] private Image uiHpFill_200;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private Sprite[] sprites;

    private GameController gameController;
    public bool hover;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        uiHpFill_100.fillAmount = (float)gameState.pHp / 100;
        uiHpFill_200.fillAmount = ((float)gameState.pHp - 100) / 100;
        text.text = gameState.pHp.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        hover = true;
        StartCoroutine(gameController.Wait(0.2f, AlphaEnter()));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        hover = false;
        StartCoroutine(AlphaExit());
    }
    IEnumerator AlphaEnter()
    {
        if (!hover)
        {
            yield break;
        }
        while (text.color != new Color(1, 1, 1, 1))
        {
            text.color = new Color(1, 1, 1, text.color.a + 0.02f);
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
    IEnumerator AlphaExit()
    {
        while (text.color != new Color(1, 1, 1, 0))
        {
            text.color = new Color(1, 1, 1, text.color.a - 0.02f);
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
    public IEnumerator HitEffectHeart(int maxTimes = 5)
    {
        int times = 0;
        while (maxTimes > times)
        {
            uiHpFill_border.sprite = sprites[1];
            uiHpFill_100.sprite = sprites[3];

            yield return new WaitForSeconds(0.1f);

            uiHpFill_100.sprite = sprites[2];
            uiHpFill_border.sprite = sprites[0];
            times += 1;
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

}
