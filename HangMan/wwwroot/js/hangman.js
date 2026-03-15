document.addEventListener("DOMContentLoaded", () => {
    const root = document.getElementById("game-root");
    if (!root) return;

    const rawWord = root.dataset.word || "";
    const word = rawWord.toLowerCase();
    const maxHealth = parseInt(root.dataset.health);
    const category = root.dataset.category || "";
    const drugClassification = root.dataset.drugClassification || "";
    const drugIsGeneric = (root.dataset.drugIsGeneric || "").toLowerCase() === "true";
    const isCustom = (root.dataset.isCustom || "").toLowerCase() === "true";
    const newGameUrl = root.dataset.newGameUrl || "";

    let health = maxHealth;
    const correct = new Set();
    const incorrect = new Set();

    // ── Header: category (left) + health boxes (right) ──
    const header = document.createElement("div");
    header.className = "game-header";

    const categoryDisplay = document.createElement("div");
    categoryDisplay.className = "category-display";
    categoryDisplay.textContent = category ? `Category: ${category}` : "";

    const healthBoxes = document.createElement("div");
    healthBoxes.className = "health-boxes";

    header.append(categoryDisplay, healthBoxes);

    // ── Word display ──
    const wordDisplay = document.createElement("div");
    wordDisplay.className = "word-display";

    // ── Incorrect guesses ──
    const incorrectDisplay = document.createElement("div");
    incorrectDisplay.className = "incorrect-display";

    // ── Guess area ──
    const guessArea = document.createElement("div");
    guessArea.className = "guess-area";

    const input = document.createElement("input");
    input.maxLength = 1;
    input.className = "guess-input";
    input.placeholder = "A–Z";

    const button = document.createElement("button");
    button.textContent = "Guess";
    button.className = "guess-button";

    guessArea.append(input, button);

    // ── Win/loss message ──
    const message = document.createElement("div");
    message.className = "game-message";

    root.append(header, wordDisplay, incorrectDisplay, guessArea, message);

    // ── Drug classification (bottom-left, drugs only) ──
    if (category.toLowerCase() === "drugs" || category.toLowerCase() === "drug") {
        const classDiv = document.createElement("div");
        classDiv.className = "drug-classification";
        classDiv.textContent = `Classification: ${drugClassification} (${drugIsGeneric ? "Generic" : "Not Generic"})`;
        root.append(classDiv);
    }

    function renderHealthBoxes() {
        healthBoxes.innerHTML = "";
        for (let i = 0; i < maxHealth; i++) {
            const box = document.createElement("div");
            box.className = "health-box " + (i < health ? "health-box--alive" : "health-box--lost");
            healthBoxes.append(box);
        }
    }

    function render() {
        wordDisplay.textContent = [...word].map(c => /\s/.test(c) ? c : (correct.has(c) ? c : "_")).join(" ");
        incorrectDisplay.textContent = incorrect.size > 0 ? `Incorrect: ${[...incorrect].join(", ")}` : "";
        renderHealthBoxes();
    }

    function submitNewGameResult(didWin) {
        if (!newGameUrl) return;

        const form = document.createElement("form");
        form.method = "post";
        form.action = newGameUrl;

        const values = {
            word: rawWord,
            mistakes: incorrect.size,
            won: didWin,
            isCustom: isCustom
        };

        Object.entries(values).forEach(([name, value]) => {
            const hidden = document.createElement("input");
            hidden.type = "hidden";
            hidden.name = name;
            hidden.value = String(value);
            form.appendChild(hidden);
        });

        document.body.appendChild(form);
        form.submit();
    }

    function showNewGameButton(didWin) {
        const newGameButton = document.createElement("button");
        newGameButton.textContent = "New Game";
        newGameButton.className = "guess-button";
        newGameButton.type = "button";
        newGameButton.addEventListener("click", () => submitNewGameResult(didWin));
        root.append(newGameButton);
    }

    function finishGame(didWin) {
        if (didWin) {
            message.textContent = "You win!";
        } else {
            message.textContent = `You lost! The word was: ${word}`;
        }

        button.disabled = true;
        input.disabled = true;
        showNewGameButton(didWin);
    }

    button.addEventListener("click", () => {
        const guess = input.value.toLowerCase();
        input.value = "";

        if (!guess.match(/^[a-z]$/)) return;
        if (correct.has(guess) || incorrect.has(guess)) return;

        if (word.includes(guess)) {
            correct.add(guess);
        } else {
            incorrect.add(guess);
            health--;
        }

        render();

        if ([...word].every(c => /\s/.test(c) || correct.has(c))) {
            finishGame(true);
        } else if (health <= 0) {
            finishGame(false);
        }
    });

    input.addEventListener("keydown", e => {
        if (e.key === "Enter") button.click();
    });

    render();
});
