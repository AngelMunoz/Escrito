// Always escape HTML for text arguments!
function escapeHtml(html) {
    const div = document.createElement('div');
    div.textContent = html;
    return div;
}

export async function updateWorkerPrompt(message, variant = 'primary', icon = 'info-circle', duration = 3000) {
    let canClose = false;

    const alert = Object.assign(document.createElement('sl-alert'), {
        variant,
        closable: true,
        duration: duration
    });
    const btnYes = Object.assign(document.createElement("sl-button"), {
        textContent: "Yes",
        onclick() {
            canClose = true;
            alert?.hide?.();
        }
    });
    const btnNo = Object.assign(document.createElement("sl-button"), {
        textContent: "No",
        onclick() {
            canClose = false;
            alert?.hide?.();
        }
    });
    const btnGroup = document.createElement('sl-button-group');
    btnGroup.append(btnYes, btnNo);

    const slIcon = Object.assign(document.createElement("sl-icon"), {
        name: icon,
        slot: "icon"
    });
    alert.append(slIcon, escapeHtml(message), btnGroup);

    document.body.appendChild(alert);

    await alert.toast();
    return canClose;
}


// Custom function to emit toast notifications
export async function notify(message, variant = 'primary', icon = 'info-circle', duration = 3000) {
    const alert = Object.assign(document.createElement('sl-alert'), {
        variant,
        closable: true,
        duration: duration
    });
    const slIcon = Object.assign(document.createElement("sl-icon"), {
        name: icon,
        slot: "icon"
    });
    alert.append(slIcon, escapeHtml(message));

    document.body.appendChild(alert);

    return await alert.toast();
}