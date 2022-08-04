const fileInput = document.querySelector("#Fotografia")
const iEliminarImg = document.querySelector("#i-eliminar-img")

if (fileInput !== null) {
    fileInput.addEventListener("change", e => {
        const img = document.querySelector("#img-preview")
        //const input = document.querySelector("#input-imagen")

        const reader = new FileReader();
        reader.onload = () => {
            fileInput.classList.add("hidden")
            img.setAttribute("src", reader.result)
            img.setAttribute("height", 200)
            //input.setAttribute("value", reader.result)
            iEliminarImg.classList.remove("hidden")
            iEliminarImg.addEventListener("click", e => {
                img.removeAttribute("src")
                img.removeAttribute("height")
                fileInput.value = null
                fileInput.classList.remove("hidden")
                e.target.classList.add("hidden")
            })
        }

        reader.readAsDataURL(e.target.files[0]);
    })
}