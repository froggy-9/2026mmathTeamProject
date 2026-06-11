using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CookieClickerUIBuilder : EditorWindow
{
    [MenuItem("Tools/Build Cookie Clicker UI")]
    public static void BuildUI()
    {
        // ----- Canvas -----
        GameObject canvasGO = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasGO.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1000, 524);

        // EventSystem
        if (Object.FindFirstObjectByType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem", typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.EventSystems.StandaloneInputModule));
        }

        // ----- Background -----
        CreateImage("Background", canvasGO.transform, new Vector2(0, 0), new Vector2(1, 1), Vector2.zero, Vector2.zero, new Color(0.2f, 0.6f, 0.3f));

        // ----- TopBar -----
        GameObject topBar = CreatePanel("TopBar", canvasGO.transform, new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, -40), new Vector2(0, 0), new Color(0.1f, 0.15f, 0.25f));
        SetAnchoredPosSize(topBar.GetComponent<RectTransform>(), Vector2.zero, new Vector2(0, 40));

        CreateText("TitleText", topBar.transform, "Cookie Clicker™", 18, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector2(10, 0), new Vector2(200, 30));
        CreateText("NewsTicker", topBar.transform, "News : various historical figures inexplicably replaced with talking lumps of dough", 14, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(500, 30));

        GameObject menuButtons = CreatePanel("MenuButtons", topBar.transform, new Vector2(1, 0.5f), new Vector2(1, 0.5f), new Vector2(-10, 0), Vector2.zero, new Color(0,0,0,0));
        SetAnchoredPosSize(menuButtons.GetComponent<RectTransform>(), new Vector2(-10, 0), new Vector2(200, 30));
        HorizontalLayoutGroup hlg = menuButtons.AddComponent<HorizontalLayoutGroup>();
        hlg.spacing = 5;
        hlg.childAlignment = TextAnchor.MiddleRight;
        string[] menuNames = { "Options", "Stats", "Info" };
        foreach (string m in menuNames)
        {
            GameObject btn = CreateButton(m, menuButtons.transform, m, 40, 30);
        }

        // ----- LeftPanel -----
        GameObject leftPanel = CreatePanel("LeftPanel", canvasGO.transform, new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, -40), Vector2.zero, new Color(0.05f, 0.05f, 0.05f, 0.3f));
        RectTransform leftRT = leftPanel.GetComponent<RectTransform>();
        leftRT.anchorMin = new Vector2(0, 0);
        leftRT.anchorMax = new Vector2(0.3f, 1);
        leftRT.offsetMin = new Vector2(0, 0);
        leftRT.offsetMax = new Vector2(0, -40);

        CreateText("BakeryNameText", leftPanel.transform, "Fantastic Computer's bakery", 16, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0, -20), new Vector2(280, 30));
        CreateText("CookieCountText", leftPanel.transform, "62.108 sextillion cookies", 22, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0, -55), new Vector2(280, 40));
        CreateText("CookiesPerSecondText", leftPanel.transform, "per second: 2.967 quintillion", 14, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0, -85), new Vector2(280, 25));

        // BigCookieButton
        GameObject bigCookie = CreateButton("BigCookieButton", leftPanel.transform, "", 220, 220);
        RectTransform bigCookieRT = bigCookie.GetComponent<RectTransform>();
        bigCookieRT.anchorMin = new Vector2(0.5f, 0.5f);
        bigCookieRT.anchorMax = new Vector2(0.5f, 0.5f);
        bigCookieRT.anchoredPosition = new Vector2(0, -20);
        bigCookie.GetComponent<Image>().color = new Color(0.55f, 0.35f, 0.2f);

        // UpgradeRing (decorative ring container behind/around cookie)
        GameObject upgradeRing = CreateImage("UpgradeRing", leftPanel.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0, -20), new Vector2(280, 280), new Color(1, 1, 1, 0.08f));

        // ----- CenterPanel -----
        GameObject centerPanel = CreatePanel("CenterPanel", canvasGO.transform, new Vector2(0.3f, 0), new Vector2(0.78f, 1), Vector2.zero, new Vector2(0, -40), new Color(0,0,0,0));
        RectTransform centerRT = centerPanel.GetComponent<RectTransform>();
        centerRT.offsetMin = new Vector2(0, 0);
        centerRT.offsetMax = new Vector2(0, -40);

        // ScrollView
        GameObject scrollView = new GameObject("BuildingScrollView", typeof(RectTransform), typeof(Image), typeof(ScrollRect));
        scrollView.transform.SetParent(centerPanel.transform, false);
        RectTransform scrollRT = scrollView.GetComponent<RectTransform>();
        scrollRT.anchorMin = Vector2.zero;
        scrollRT.anchorMax = Vector2.one;
        scrollRT.offsetMin = Vector2.zero;
        scrollRT.offsetMax = Vector2.zero;
        scrollView.GetComponent<Image>().color = new Color(0.3f, 0.5f, 0.7f, 0.3f);

        // Viewport
        GameObject viewport = new GameObject("Viewport", typeof(RectTransform), typeof(Image), typeof(Mask));
        viewport.transform.SetParent(scrollView.transform, false);
        RectTransform viewportRT = viewport.GetComponent<RectTransform>();
        viewportRT.anchorMin = Vector2.zero;
        viewportRT.anchorMax = Vector2.one;
        viewportRT.offsetMin = new Vector2(0, 0);
        viewportRT.offsetMax = new Vector2(-20, 0); // leave room for scrollbar
        viewport.GetComponent<Image>().color = new Color(1, 1, 1, 0.01f);
        viewport.GetComponent<Mask>().showMaskGraphic = false;

        // Content
        GameObject content = new GameObject("Content", typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
        content.transform.SetParent(viewport.transform, false);
        RectTransform contentRT = content.GetComponent<RectTransform>();
        contentRT.anchorMin = new Vector2(0, 1);
        contentRT.anchorMax = new Vector2(1, 1);
        contentRT.pivot = new Vector2(0.5f, 1f);
        contentRT.anchoredPosition = Vector2.zero;
        contentRT.sizeDelta = new Vector2(0, 0);

        VerticalLayoutGroup vlg = content.GetComponent<VerticalLayoutGroup>();
        vlg.spacing = 5;
        vlg.padding = new RectOffset(5, 5, 5, 5);
        vlg.childForceExpandHeight = false;
        vlg.childForceExpandWidth = true;
        vlg.childControlHeight = false;
        vlg.childControlWidth = true;

        ContentSizeFitter csf = content.GetComponent<ContentSizeFitter>();
        csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        // Building rows
        string[] buildingNames = { "Cursor", "Grandma", "Farm", "Mine", "Factory", "Bank", "Temple", "Wizard Tower", "Shipment", "Alchemy Lab", "Portal" };
        foreach (string b in buildingNames)
        {
            string rowName = b.Replace(" ", "") + "Row";
            GameObject row = new GameObject(rowName, typeof(RectTransform), typeof(Image), typeof(LayoutElement));
            row.transform.SetParent(content.transform, false);
            row.GetComponent<Image>().color = Color.white;
            LayoutElement le = row.GetComponent<LayoutElement>();
            le.preferredHeight = 60;
            le.minHeight = 60;

            CreateText("Label", row.transform, b, 16, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector2(10, 0), new Vector2(200, 30), Color.black);
        }

        // ScrollRect setup
        ScrollRect sr = scrollView.GetComponent<ScrollRect>();
        sr.viewport = viewportRT;
        sr.content = contentRT;
        sr.horizontal = false;
        sr.vertical = true;
        sr.movementType = ScrollRect.MovementType.Clamped;
        sr.scrollSensitivity = 20;

        // Vertical Scrollbar
        GameObject scrollbar = new GameObject("Scrollbar Vertical", typeof(RectTransform), typeof(Image), typeof(Scrollbar));
        scrollbar.transform.SetParent(scrollView.transform, false);
        RectTransform sbRT = scrollbar.GetComponent<RectTransform>();
        sbRT.anchorMin = new Vector2(1, 0);
        sbRT.anchorMax = new Vector2(1, 1);
        sbRT.pivot = new Vector2(1, 1);
        sbRT.anchoredPosition = Vector2.zero;
        sbRT.sizeDelta = new Vector2(18, 0);
        scrollbar.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.5f);

        GameObject sbSlidingArea = new GameObject("Sliding Area", typeof(RectTransform));
        sbSlidingArea.transform.SetParent(scrollbar.transform, false);
        RectTransform slidingRT = sbSlidingArea.GetComponent<RectTransform>();
        slidingRT.anchorMin = Vector2.zero;
        slidingRT.anchorMax = Vector2.one;
        slidingRT.offsetMin = new Vector2(2, 2);
        slidingRT.offsetMax = new Vector2(-2, -2);

        GameObject sbHandle = new GameObject("Handle", typeof(RectTransform), typeof(Image));
        sbHandle.transform.SetParent(sbSlidingArea.transform, false);
        RectTransform handleRT = sbHandle.GetComponent<RectTransform>();
        handleRT.sizeDelta = new Vector2(0, 0);
        sbHandle.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 0.8f);

        Scrollbar sb = scrollbar.GetComponent<Scrollbar>();
        sb.handleRect = handleRT;
        sb.targetGraphic = sbHandle.GetComponent<Image>();
        sb.direction = Scrollbar.Direction.TopToBottom;

        sr.verticalScrollbar = sb;
        sr.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;
        sr.verticalScrollbarSpacing = -3;

        // Adjust viewport offsetMax to account for scrollbar width
        viewportRT.offsetMax = new Vector2(-18, 0);

        // ----- RightPanel -----
        GameObject rightPanel = CreatePanel("RightPanel", canvasGO.transform, new Vector2(0.78f, 0), new Vector2(1f, 1f), Vector2.zero, new Vector2(0, -40), new Color(0.15f, 0.15f, 0.15f, 0.5f));
        RectTransform rightRT = rightPanel.GetComponent<RectTransform>();
        rightRT.offsetMin = Vector2.zero;
        rightRT.offsetMax = new Vector2(0, -40);

        CreateText("StoreTitle", rightPanel.transform, "Store", 22, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0, -20), new Vector2(150, 30));

        // UpgradeGrid
        GameObject upgradeGrid = new GameObject("UpgradeGrid", typeof(RectTransform), typeof(Image), typeof(GridLayoutGroup));
        upgradeGrid.transform.SetParent(rightPanel.transform, false);
        RectTransform ugRT = upgradeGrid.GetComponent<RectTransform>();
        ugRT.anchorMin = new Vector2(0, 1);
        ugRT.anchorMax = new Vector2(1, 1);
        ugRT.pivot = new Vector2(0.5f, 1f);
        ugRT.anchoredPosition = new Vector2(0, -50);
        ugRT.sizeDelta = new Vector2(-10, 90);
        upgradeGrid.GetComponent<Image>().color = new Color(1, 1, 1, 0.1f);
        GridLayoutGroup glg = upgradeGrid.GetComponent<GridLayoutGroup>();
        glg.cellSize = new Vector2(40, 40);
        glg.spacing = new Vector2(4, 4);
        glg.padding = new RectOffset(5, 5, 5, 5);
        for (int i = 0; i < 10; i++)
        {
            GameObject slot = new GameObject("UpgradeSlot" + i, typeof(RectTransform), typeof(Image));
            slot.transform.SetParent(upgradeGrid.transform, false);
            slot.GetComponent<Image>().color = Color.white;
        }

        // ProductScrollView
        GameObject prodScroll = new GameObject("ProductScrollView", typeof(RectTransform), typeof(Image), typeof(ScrollRect));
        prodScroll.transform.SetParent(rightPanel.transform, false);
        RectTransform prodRT = prodScroll.GetComponent<RectTransform>();
        prodRT.anchorMin = new Vector2(0, 0);
        prodRT.anchorMax = new Vector2(1, 1);
        prodRT.offsetMin = new Vector2(5, 5);
        prodRT.offsetMax = new Vector2(-5, -150);
        prodScroll.GetComponent<Image>().color = new Color(1, 1, 1, 0.05f);

        GameObject prodViewport = new GameObject("Viewport", typeof(RectTransform), typeof(Image), typeof(Mask));
        prodViewport.transform.SetParent(prodScroll.transform, false);
        RectTransform prodViewportRT = prodViewport.GetComponent<RectTransform>();
        prodViewportRT.anchorMin = Vector2.zero;
        prodViewportRT.anchorMax = Vector2.one;
        prodViewportRT.offsetMin = Vector2.zero;
        prodViewportRT.offsetMax = new Vector2(-18, 0);
        prodViewport.GetComponent<Image>().color = new Color(1, 1, 1, 0.01f);
        prodViewport.GetComponent<Mask>().showMaskGraphic = false;

        GameObject prodContent = new GameObject("Content", typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
        prodContent.transform.SetParent(prodViewport.transform, false);
        RectTransform prodContentRT = prodContent.GetComponent<RectTransform>();
        prodContentRT.anchorMin = new Vector2(0, 1);
        prodContentRT.anchorMax = new Vector2(1, 1);
        prodContentRT.pivot = new Vector2(0.5f, 1f);
        prodContentRT.anchoredPosition = Vector2.zero;
        prodContentRT.sizeDelta = new Vector2(0, 0);

        VerticalLayoutGroup prodVlg = prodContent.GetComponent<VerticalLayoutGroup>();
        prodVlg.spacing = 5;
        prodVlg.padding = new RectOffset(5, 5, 5, 5);
        prodVlg.childForceExpandHeight = false;
        prodVlg.childForceExpandWidth = true;
        prodVlg.childControlHeight = false;
        prodVlg.childControlWidth = true;

        ContentSizeFitter prodCsf = prodContent.GetComponent<ContentSizeFitter>();
        prodCsf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        for (int i = 0; i < 8; i++)
        {
            GameObject prodRow = new GameObject("ProductRow" + i, typeof(RectTransform), typeof(Image), typeof(LayoutElement));
            prodRow.transform.SetParent(prodContent.transform, false);
            prodRow.GetComponent<Image>().color = Color.white;
            LayoutElement prodLe = prodRow.GetComponent<LayoutElement>();
            prodLe.preferredHeight = 50;
            prodLe.minHeight = 50;
            CreateText("Label", prodRow.transform, "Item " + (i + 1), 14, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector2(10, 0), new Vector2(150, 25), Color.black);
        }

        ScrollRect prodSr = prodScroll.GetComponent<ScrollRect>();
        prodSr.viewport = prodViewportRT;
        prodSr.content = prodContentRT;
        prodSr.horizontal = false;
        prodSr.vertical = true;
        prodSr.movementType = ScrollRect.MovementType.Clamped;
        prodSr.scrollSensitivity = 20;

        GameObject prodScrollbar = new GameObject("Scrollbar Vertical", typeof(RectTransform), typeof(Image), typeof(Scrollbar));
        prodScrollbar.transform.SetParent(prodScroll.transform, false);
        RectTransform prodSbRT = prodScrollbar.GetComponent<RectTransform>();
        prodSbRT.anchorMin = new Vector2(1, 0);
        prodSbRT.anchorMax = new Vector2(1, 1);
        prodSbRT.pivot = new Vector2(1, 1);
        prodSbRT.anchoredPosition = Vector2.zero;
        prodSbRT.sizeDelta = new Vector2(18, 0);
        prodScrollbar.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.5f);

        GameObject prodSlidingArea = new GameObject("Sliding Area", typeof(RectTransform));
        prodSlidingArea.transform.SetParent(prodScrollbar.transform, false);
        RectTransform prodSlidingRT = prodSlidingArea.GetComponent<RectTransform>();
        prodSlidingRT.anchorMin = Vector2.zero;
        prodSlidingRT.anchorMax = Vector2.one;
        prodSlidingRT.offsetMin = new Vector2(2, 2);
        prodSlidingRT.offsetMax = new Vector2(-2, -2);

        GameObject prodSbHandle = new GameObject("Handle", typeof(RectTransform), typeof(Image));
        prodSbHandle.transform.SetParent(prodSlidingArea.transform, false);
        RectTransform prodHandleRT = prodSbHandle.GetComponent<RectTransform>();
        prodHandleRT.sizeDelta = new Vector2(0, 0);
        prodSbHandle.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 0.8f);

        Scrollbar prodSb = prodScrollbar.GetComponent<Scrollbar>();
        prodSb.handleRect = prodHandleRT;
        prodSb.targetGraphic = prodSbHandle.GetComponent<Image>();
        prodSb.direction = Scrollbar.Direction.TopToBottom;

        prodSr.verticalScrollbar = prodSb;
        prodSr.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;
        prodSr.verticalScrollbarSpacing = -3;

        // ----- PopupLayer -----
        GameObject popupLayer = CreatePanel("PopupLayer", canvasGO.transform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero, new Color(0,0,0,0));
        RectTransform popupRT = popupLayer.GetComponent<RectTransform>();
        popupRT.offsetMin = Vector2.zero;
        popupRT.offsetMax = Vector2.zero;

        // Tooltip
        GameObject tooltip = CreateImage("Tooltip", popupLayer.transform, new Vector2(0, 0), new Vector2(0, 0), new Vector2(50, 50), new Vector2(200, 80), new Color(0.1f, 0.1f, 0.1f, 0.9f));
        CreateText("TooltipText", tooltip.transform, "Tooltip info...", 14, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(180, 60));
        tooltip.SetActive(false);

        // GoldenCookie
        GameObject goldenCookie = CreateButton("GoldenCookie", popupLayer.transform, "", 64, 64);
        goldenCookie.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.7f);
        goldenCookie.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.7f);
        goldenCookie.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        goldenCookie.GetComponent<Image>().color = new Color(1f, 0.85f, 0.2f);
        goldenCookie.SetActive(false);

        // Notification
        GameObject notification = CreateImage("Notification", popupLayer.transform, new Vector2(1, 1), new Vector2(1, 1), new Vector2(-150, -50), new Vector2(280, 70), new Color(0.95f, 0.95f, 0.85f, 0.95f));
        CreateText("NotificationText", notification.transform, "Achievement unlocked!", 14, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(260, 50), Color.black);
        notification.SetActive(false);

        Debug.Log("Cookie Clicker UI Hierarchy Built Successfully!");
    }

    // ---------- Helper Methods ----------

    static GameObject CreatePanel(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPos, Vector2 sizeDelta, Color color)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.anchoredPosition = anchoredPos;
        rt.sizeDelta = sizeDelta;
        go.GetComponent<Image>().color = color;
        return go;
    }

    static GameObject CreateImage(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPos, Vector2 sizeDelta, Color color)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.anchoredPosition = anchoredPos;
        rt.sizeDelta = sizeDelta;
        go.GetComponent<Image>().color = color;
        return go;
    }

    static void SetAnchoredPosSize(RectTransform rt, Vector2 anchoredPos, Vector2 sizeDelta)
    {
        rt.anchoredPosition = anchoredPos;
        rt.sizeDelta = sizeDelta;
    }

    static GameObject CreateText(string name, Transform parent, string text, float fontSize, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPos, Vector2 sizeDelta)
    {
        return CreateText(name, parent, text, fontSize, anchorMin, anchorMax, anchoredPos, sizeDelta, Color.white);
    }

    static GameObject CreateText(string name, Transform parent, string text, float fontSize, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPos, Vector2 sizeDelta, Color color)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
        go.transform.SetParent(parent, false);
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.anchoredPosition = anchoredPos;
        rt.sizeDelta = sizeDelta;
        TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.color = color;
        tmp.alignment = TextAlignmentOptions.MidlineLeft;
        return go;
    }

    static GameObject CreateButton(string name, Transform parent, string label, float width, float height)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
        go.transform.SetParent(parent, false);
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(width, height);
        go.GetComponent<Image>().color = Color.white;

        if (!string.IsNullOrEmpty(label))
        {
            GameObject textGO = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
            textGO.transform.SetParent(go.transform, false);
            RectTransform textRT = textGO.GetComponent<RectTransform>();
            textRT.anchorMin = Vector2.zero;
            textRT.anchorMax = Vector2.one;
            textRT.offsetMin = Vector2.zero;
            textRT.offsetMax = Vector2.zero;
            TextMeshProUGUI tmp = textGO.GetComponent<TextMeshProUGUI>();
            tmp.text = label;
            tmp.fontSize = 14;
            tmp.color = Color.black;
            tmp.alignment = TextAlignmentOptions.Midline;
        }
        return go;
    }
}