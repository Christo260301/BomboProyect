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


const removeImage = () => {
    const img = document.querySelector("#img-preview")
    img.removeAttribute("src")
    img.removeAttribute("style")
    fileInput.value = null
    fileInput.classList.remove("hidden")
    iEliminarImg.classList.add("hidden")
}

let listJsonInsumos = []

const addInsumo = (jsonInsumo) => {
    //VALIDAR INSUMO YA SELECCIONADO
    if (listJsonInsumos.find(x => x.InsumoId == jsonInsumo.InsumoId) == undefined) {
        listJsonInsumos.push(jsonInsumo)

        console.log(jsonInsumo);
        const ContElement = document.querySelector("#ContElement")
        const ContElmeJson = document.querySelector("#ContElmeJson")
        ContElmeJson.value = JSON.stringify(listJsonInsumos)
        const tBodyContainer = document.querySelector("#tbContainer")
        let NumRow = parseInt(ContElement.value)

        console.log(ContElement)
        // TROW
        const trow = document.createElement("tr")
        // TD ID
        const tdId = document.createElement("td")
        tdId.setAttribute("hidden", true)
        const intId = document.createElement("input")
        intId.setAttribute("name", `[${NumRow}].InsumoId`)
        intId.setAttribute("value", jsonInsumo.InsumoId)
        intId.setAttribute("type", "hidden")
        tdId.insertAdjacentElement("afterbegin", intId)
        const tdNom = document.createElement("td")
        tdNom.innerText = jsonInsumo.Nombre
        const tdCant = document.createElement("td")
        const intCant = document.createElement("input")
        intCant.setAttribute("type", "number")
        intCant.setAttribute("name", `[${NumRow}].ContenidoTot`)
        intCant.setAttribute("value", "0")
        intCant.setAttribute("step", "0.01")
        tdCant.insertAdjacentElement("afterbegin", intCant)
        const tdAccion = document.createElement("td")
        tdAccion.innerHTML = `
        <div class="d-flex justify-content-start">
            <button type="button" onclick="removeInsumo(\'${JSON.stringify(jsonInsumo).replace(/"/g, "#")}\')" class="btn btn-danger">
                <i class="fa-solid fa-minus"></i>
            </button>
         </div>
        `

        trow.setAttribute("id", `ID_${jsonInsumo.InsumoId}_${jsonInsumo.Nombre}`)

        trow.insertAdjacentElement("afterbegin", tdAccion)
        trow.insertAdjacentElement("afterbegin", tdCant)
        trow.insertAdjacentElement("afterbegin", tdNom)
        trow.insertAdjacentElement("afterbegin", tdId)

        tBodyContainer.insertAdjacentElement("beforeend", trow)
        ContElement.value = (NumRow + 1)
    }
}

const removeInsumo = (jsonInsumo) => {
    // ELIMINACION DEL INSUMO DE LA LISTA JSON
    console.log(JSON.parse(jsonInsumo.replace(/#/g, '\"')))
    jsInsu = JSON.parse(jsonInsumo.replace(/#/g, '\"'))
    const idx = listJsonInsumos.findIndex(x => x.InsumoId == jsInsu.InsumoId);
    if (idx != -1) {
        listJsonInsumos.splice(idx, 1)
        const row = document.querySelector(`#ID_${jsInsu.InsumoId}_${jsInsu.Nombre}`)
        row.remove()
        // ELIMINA EL ROW DE LA LISTA

        // ESTABLECIMIENTO DE INDICES
        const tBodyContainer = document.querySelector("#tbContainer")
        console.log(tBodyContainer)
        const listRow = tBodyContainer.children
        console.log(listRow)
        console.log(listRow.length)

        for (let i = 0; i < listRow.length; i++) {
            console.log('ITEM => ', listRow[i])
            listRow[i].children[0].children[0].setAttribute("name", `[${i}].InsumoId`)
            listRow[i].children[2].children[0].setAttribute("name", `[${i}].ContenidoTot`)
        }
    }
}