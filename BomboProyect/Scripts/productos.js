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
    console.log(jsonInsumo)
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
        // Campos completos de insumo
        const tdDataIsumo = document.createElement("td")
        tdDataIsumo.setAttribute("hidden", true)
        
        const intId = document.createElement("input")
        const intNombre = document.createElement("input")
        const intDescripcion = document.createElement("input")
        const intPrecio = document.createElement("input")
        const intUnidad = document.createElement("input")
        const intCantidadNeta = document.createElement("input")
        const intContenidoTot = document.createElement("input")
        const intExistencias = document.createElement("input")
        const intStatus = document.createElement("input")

        // STATUS DEL INSUMO
        intStatus.setAttribute("name", `[${NumRow}].Status`)
        intStatus.setAttribute("value", jsonInsumo.Status)
        intStatus.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intStatus)

        // CANTIDADTOT DEL INSUMO
        intContenidoTot.setAttribute("name", `[${NumRow}].ContenidoTot`)
        intContenidoTot.setAttribute("value", jsonInsumo.ContenidoTot)
        intContenidoTot.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intContenidoTot)

        // EXISTENCIAS DEL INSUMO
        intExistencias.setAttribute("name", `[${NumRow}].Existencias`)
        intExistencias.setAttribute("value", jsonInsumo.Existencias)
        intExistencias.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intExistencias)

        // CANTIDAD NETA DEL INSUMO
        intCantidadNeta.setAttribute("name", `[${NumRow}].CantidadNeta`)
        intCantidadNeta.setAttribute("value", jsonInsumo.CantidadNeta)
        intCantidadNeta.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intCantidadNeta)

        // UNIDAD DE INSUMO
        intUnidad.setAttribute("name", `[${NumRow}].Unidad`)
        intUnidad.setAttribute("value", jsonInsumo.Unidad)
        intUnidad.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intUnidad)

        // PRECION DE INSUMO
        intPrecio.setAttribute("name", `[${NumRow}].Precio`)
        intPrecio.setAttribute("value", jsonInsumo.Precio)
        intPrecio.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intPrecio)

        // DESCRIPCION DE INSUMO 
        intDescripcion.setAttribute("name", `[${NumRow}].Descripcion`)
        intDescripcion.setAttribute("value", jsonInsumo.Descripcion)
        intDescripcion.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intDescripcion)

        // NOMBRE DE INSUMO
        intNombre.setAttribute("name", `[${NumRow}].Nombre`)
        intNombre.setAttribute("value", jsonInsumo.Nombre)
        intNombre.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intNombre)

        // ID DE INSUMO
        intId.setAttribute("name", `[${NumRow}].InsumoId`)
        intId.setAttribute("value", jsonInsumo.InsumoId)
        intId.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intId)

        const tdNom = document.createElement("td")
        tdNom.innerText = jsonInsumo.Nombre
        const tdCant = document.createElement("td")
        const intCant = document.createElement("input")
        intCant.setAttribute("type", "number")
        intCant.setAttribute("name", `[${NumRow}].CantProduc`)
        intCant.setAttribute("min", "0")
        intCant.setAttribute("value", 0.123)
        intCant.setAttribute("step", "any")
        intCant.setAttribute("class", "form-control")
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
        trow.insertAdjacentElement("afterbegin", tdDataIsumo)

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