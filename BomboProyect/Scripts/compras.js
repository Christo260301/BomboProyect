let listJsonInsumos = []
let listJsonDetRemove = []

const addInsumo = (jsonInsumo, isEdit) => {
    console.log(jsonInsumo)

    if (listJsonInsumos.find(x => x == jsonInsumo.InsumoId) == undefined) {
        listJsonInsumos.push(jsonInsumo.InsumoId)

        const idxR = listJsonDetRemove.findIndex(x => x == jsonInsumo.InsumoID)
        if (idxR != -1) {
            listJsonDetRemove.splice(idxR, 1)
            const InsumosRemovidos = document.querySelector('#InsumosRemovidos')
            InsumosRemovidos.value = JSON.stringify(listJsonDetRemove)
        }

        console.log(jsonInsumo);
        const ContElement = document.querySelector("#ContElement")
        const ContElmeJson = document.querySelector("#ContElmeJson")
        ContElmeJson.value = JSON.stringify(listJsonInsumos)
        const tBodyContainer = document.querySelector("#tbContainer")
        let NumRow = parseInt(ContElement.value)

        console.log(ContElement)

        const trow = document.createElement("tr")
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
        intStatus.setAttribute("name", !isEdit ? `[${NumRow}].Status` : `[${NumRow}].Insumo.Status`)
        intStatus.setAttribute("value", jsonInsumo.Status)
        intStatus.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intStatus)

        // CANTIDADTOT DEL INSUMO
        intContenidoTot.setAttribute("name", !isEdit ? `[${NumRow}].ContenidoTot` : `[${NumRow}].Insumo.ContenidoTot`)
        intContenidoTot.setAttribute("value", jsonInsumo.ContenidoTot)
        intContenidoTot.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intContenidoTot)

        // EXISTENCIAS DEL INSUMO
        intExistencias.setAttribute("name", !isEdit ? `[${NumRow}].Existencias` : `[${NumRow}].Insumo.Existencias`)
        intExistencias.setAttribute("value", jsonInsumo.Existencias)
        intExistencias.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intExistencias)

        // CANTIDAD NETA DEL INSUMO
        intCantidadNeta.setAttribute("name", !isEdit ? `[${NumRow}].CantidadNeta` : `[${NumRow}].Insumo.CantidadNeta`)
        intCantidadNeta.setAttribute("value", jsonInsumo.CantidadNeta)
        intCantidadNeta.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intCantidadNeta)

        // UNIDAD DE INSUMO
        intUnidad.setAttribute("name", !isEdit ? `[${NumRow}].Unidad` : `[${NumRow}].Insumo.Unidad`)
        intUnidad.setAttribute("value", jsonInsumo.Unidad)
        intUnidad.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intUnidad)

        // PRECION DE INSUMO
        intPrecio.setAttribute("name", !isEdit ? `[${NumRow}].Precio` : `[${NumRow}].Insumo.Precio`)
        intPrecio.setAttribute("value", jsonInsumo.Precio)
        intPrecio.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intPrecio)

        // DESCRIPCION DE INSUMO 
        intDescripcion.setAttribute("name", !isEdit ? `[${NumRow}].Descripcion` : `[${NumRow}].Insumo.Descripcion`)
        intDescripcion.setAttribute("value", jsonInsumo.Descripcion)
        intDescripcion.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intDescripcion)

        // NOMBRE DE INSUMO
        intNombre.setAttribute("name", !isEdit ? `[${NumRow}].Nombre` : `[${NumRow}].Insumo.Nombre`)
        intNombre.setAttribute("value", jsonInsumo.Nombre)
        intNombre.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intNombre)

        // ID DE INSUMO
        intId.setAttribute("name", !isEdit ? `[${NumRow}].InsumoId` : `[${NumRow}].Insumo.InsumoId`)
        intId.setAttribute("value", jsonInsumo.InsumoId)
        intId.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intId)

        const tdNom = document.createElement("td")
        tdNom.innerText = jsonInsumo.Nombre
    
        const tdCant = document.createElement("td")
        const intCant = document.createElement("input")
        intCant.setAttribute("type", "number")
        intCant.setAttribute("name", !isEdit ? `[${NumRow}].CantProduc` : `[${NumRow}].Insumo.CantProduc`)
        intCant.setAttribute("min", "0")
        intCant.setAttribute("value", 0.0)
        intCant.setAttribute("step", "any")
        intCant.setAttribute("class", "form-control")
        tdCant.insertAdjacentElement("afterbegin", intCant)

        const tdFecha = document.createElement("td")
        const intFecha = document.createElement("input")
        intFecha.setAttribute("type", "date")
        intFecha.setAttribute("name", `[${NumRow}].FechaCad`)
        intFecha.setAttribute("value", "")
        intFecha.setAttribute("class", "form-control")
        tdFecha.insertAdjacentElement("afterbegin", intFecha)

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
        trow.insertAdjacentElement("afterbegin", tdFecha)
        trow.insertAdjacentElement("afterbegin", tdNom)
        trow.insertAdjacentElement("afterbegin", tdDataIsumo)

        tBodyContainer.insertAdjacentElement("beforeend", trow)
        ContElement.value = (NumRow + 1)
        
    }
}

const removeInsumo = (jsonInsumo, isEdit) => {
    // ELIMINACION DEL INSUMO DE LA LISTA JSON
    // console.log(JSON.parse(jsonInsumo.replace(/#/g, '\"')))

    let jsInsu = {}
    try {
        jsInsu = JSON.parse(jsonInsumo.replace(/#/g, '\"'))
        console.log(jsInsu)
    } catch (e) {
        console.log("ERR => ", e)
        jsInsu = jsonInsumo
    }

    const idx = listJsonInsumos.findIndex(x => x == jsInsu.InsumoId);
    if (idx != -1) {

        // AGREGA DETALLE A LA LISTA DE REMOVIDOS
        listJsonDetRemove.push(jsonInsumo.InsumoId)
        const InsumosRemovidos = document.querySelector("#InsumosRemovidos")
        InsumosRemovidos.value = JSON.stringify(listJsonDetRemove)
        console.log(listJsonDetRemove)

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
        if (!isEdit) {
            for (let i = 0; i < listRow.length; i++) {
                console.log('ITEM => ', listRow[i])
                listRow[i].children[0].children[0].setAttribute("name", `[${i}].InsumoId`)
                listRow[i].children[0].children[1].setAttribute("name", `[${i}].Nombre`)
                listRow[i].children[0].children[2].setAttribute("name", `[${i}].Descripcion`)
                listRow[i].children[0].children[3].setAttribute("name", `[${i}].Precio`)
                listRow[i].children[0].children[4].setAttribute("name", `[${i}].Unidad`)
                listRow[i].children[0].children[5].setAttribute("name", `[${i}].CantidadNeta`)
                listRow[i].children[0].children[6].setAttribute("name", `[${i}].Existencias`)
                listRow[i].children[0].children[7].setAttribute("name", `[${i}].ContenidoTot`)
                listRow[i].children[0].children[8].setAttribute("name", `[${i}].Status`)

                listRow[i].children[2].children[0].setAttribute("name", `[${i}].CantProduc`)
                listRow[i].children[2].children[0].setAttribute("name", `[${i}].FechaCad`)
            }
        } else {
            for (let i = 0; i < listRow.length; i++) {
                console.log('ITEM => ', listRow[i])
                listRow[i].children[0].children[0].setAttribute("name", `[${i}].Insumo.InsumoId`)
                listRow[i].children[0].children[1].setAttribute("name", `[${i}].Insumo.Nombre`)
                listRow[i].children[0].children[2].setAttribute("name", `[${i}].Insumo.Descripcion`)
                listRow[i].children[0].children[3].setAttribute("name", `[${i}].Insumo.Precio`)
                listRow[i].children[0].children[4].setAttribute("name", `[${i}].Insumo.Unidad`)
                listRow[i].children[0].children[5].setAttribute("name", `[${i}].Insumo.CantidadNeta`)
                listRow[i].children[0].children[6].setAttribute("name", `[${i}].Insumo.Existencias`)
                listRow[i].children[0].children[7].setAttribute("name", `[${i}].Insumo.ContenidoTot`)
                listRow[i].children[0].children[8].setAttribute("name", `[${i}].Insumo.Status`)

                listRow[i].children[2].children[0].setAttribute("name", `[${i}].Insumo.CantProduc`)
            }
        }


        const ContElementNum = document.querySelector("#ContElement")
        ContElementNum.value = listRow.length

        const CntElementJson = document.querySelector("#ContElmeJson")
        CntElementJson.value = JSON.stringify(listJsonInsumos)
    }
}
